using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LineRenderer m_Laser;
    public float m_MaxDistance = 250f;
    public float m_DotLaserAlive = 0.86f;
    public int m_BulletDamage = 20;
    public float m_FireRate = 1.0f;
    public float m_NextTimeToFire = 0.0f;
    public LayerMask m_LayerMask;
    public AudioSource m_MachineGun;

    void Update()
    {
        bool l_LaserAlive = Vector3.Dot(transform.up, Vector3.up) >= m_DotLaserAlive;
        m_Laser.gameObject.SetActive(l_LaserAlive);

        if (l_LaserAlive)
        {
            RaycastHit l_RaycastHit;
            Ray l_Ray = new Ray(m_Laser.transform.position, m_Laser.transform.forward);
            float l_Distance = m_MaxDistance;
            if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_LayerMask))
                l_Distance = l_RaycastHit.distance;
            m_Laser.SetPosition(1, new Vector3(0.0f, 0.0f, l_Distance));

            FPSController target = l_RaycastHit.transform.GetComponentInParent<FPSController>();
            if (target != null)
            {
                if (Time.time >= m_NextTimeToFire)
                {
                    m_NextTimeToFire = Time.time + 1f / m_FireRate;

                    Shoot(target);
                }
            }
        }


    }

    private void Shoot(FPSController _Target)
    {
        m_MachineGun.Play();
        _Target.HurtingPlayer(m_BulletDamage);
    }
}
