using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LineRenderer m_Laser;
    public float m_MaxDistance = 250f;
    public float m_DotLaserAlive = 0.86f;
    public LayerMask m_LayerMask;

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
        }


    }
}
