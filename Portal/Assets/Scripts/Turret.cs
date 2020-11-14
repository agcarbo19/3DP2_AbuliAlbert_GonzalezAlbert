using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float m_MaxDistance = 250f;
    public float m_DotLaserAlive = 0.86f;
    public float m_FireRate = 1.0f;
    public float m_NextTimeToFire = 0.0f;
    public float m_ImpactForce = 30f;
    public int m_Health = 100;
    public int m_BulletDamage = 100;
    public LayerMask m_LayerMask;
    public LineRenderer m_Laser;
    public AudioSource m_MachineGun;
    public ParticleSystem m_Explosion;
    public RefractionCube m_RefractionCubeHit;

    void Update()
    {
        bool l_LaserAlive = Vector3.Dot(transform.forward, Vector3.up) >= m_DotLaserAlive;
        m_Laser.gameObject.SetActive(l_LaserAlive);
        RefractionCube l_RefractionCube = m_RefractionCubeHit;
        m_RefractionCubeHit = null;

        if (l_LaserAlive)
        {
            RaycastHit l_RaycastHit;
            Ray l_Ray = new Ray(m_Laser.transform.position, m_Laser.transform.forward);
            float l_Distance = m_MaxDistance;
            if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_LayerMask))
            {
                l_Distance = l_RaycastHit.distance;

                if (l_RaycastHit.collider.tag == "Player" || l_RaycastHit.collider.tag == "EnemyTurret")
                {
                    if (Time.time >= m_NextTimeToFire)
                    {
                        m_NextTimeToFire = Time.time + 1f / m_FireRate;
                        Shoot(l_RaycastHit.transform);
                        if (l_RaycastHit.collider.tag == "EnemyTurret")
                            l_RaycastHit.collider.GetComponent<Rigidbody>().AddForce(-l_RaycastHit.normal * m_ImpactForce);
                    }
                }

                if (l_RaycastHit.collider.tag == "RefractionCube")
                {
                    m_RefractionCubeHit = l_RaycastHit.collider.GetComponent<RefractionCube>();
                    m_RefractionCubeHit.Reflection(gameObject);
                }
                if (l_RaycastHit.collider.tag == "PortalRefection")
                {
                    SubPortal l_Portal = l_RaycastHit.collider.transform.parent.GetComponent<SubPortal>();
                    l_Portal.Reflection(l_RaycastHit.point, l_Ray.direction);
                }
            }
            m_Laser.SetPosition(1, new Vector3(0f, 0f, l_Distance));
        }

        if (l_RefractionCube != m_RefractionCubeHit && l_RefractionCube!= null)
        {
            l_RefractionCube.StopReflection();
        }

        if (m_Health <= 0)
        {
            GameObject.Instantiate(m_Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Damage)
    {
        m_Health -= Damage;
    }

    private void Shoot(Transform _Target)
    {
        m_MachineGun.Play();
        if (_Target.GetComponentInParent<FPSController>())
            _Target.GetComponentInParent<FPSController>().HurtingPlayer(m_BulletDamage);

        if (_Target.tag == "EnemyTurret")
            _Target.GetComponent<Turret>().TakeDamage(m_BulletDamage);
    }
}
