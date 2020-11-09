using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LineRenderer m_Laser;
    public float m_MaxDistance = 250f;
    public float m_DotLaserAlive = 0.86f;
    public int m_BulletDamage = 100;
    public float m_FireRate = 1.0f;
    public float m_NextTimeToFire = 0.0f;
    public float m_ImpactForce = 30f;
    public LayerMask m_LayerMask;
    public AudioSource m_MachineGun;

    void Update()
    {
        bool l_LaserAlive = Vector3.Dot(transform.forward, Vector3.up) >= m_DotLaserAlive;
        m_Laser.gameObject.SetActive(l_LaserAlive);

        if (l_LaserAlive)
        {
            RaycastHit l_RaycastHit;
            Ray l_Ray = new Ray(m_Laser.transform.position, m_Laser.transform.forward);
            float l_Distance = m_MaxDistance;
            if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_LayerMask))
                l_Distance = l_RaycastHit.distance;
            m_Laser.SetPosition(1, new Vector3(0.0f, 0.0f, l_Distance));

            Transform target = l_RaycastHit.transform;

            if (target.GetComponentInParent<FPSController>() || target.tag == "EnemyTurret")
            {
                if (Time.time >= m_NextTimeToFire)
                {
                    m_NextTimeToFire = Time.time + 1f / m_FireRate;
                    Shoot(target);
                    if (target.tag == "EnemyTurret")
                        target.GetComponent<Rigidbody>().AddForce(-l_RaycastHit.normal * m_ImpactForce);
                }
            }
        }


    }

    public void TakeDamage(int Damage)
    {

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
