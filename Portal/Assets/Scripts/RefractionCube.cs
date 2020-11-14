using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionCube : MonoBehaviour
{
    public LineRenderer m_Laser;
    public LayerMask m_LayerLaserMask;
    public float m_MaxDistance = 250f;
    public RefractionCube m_RefractionCubeHit;
    public GameObject m_ReflectionEmiter;

    void Start()
    {
        m_Laser.gameObject.SetActive(false);
    }

    void Update()
    {
        if (m_Laser.gameObject.activeSelf)
            UpdateLaserDistance();
    }

    public bool Reflection(GameObject _ReflectionEmiter)
    {
        if (m_ReflectionEmiter != null && _ReflectionEmiter != m_ReflectionEmiter)
        {
            m_ReflectionEmiter = null;
            return false;
        }

        m_ReflectionEmiter = _ReflectionEmiter;
        m_Laser.gameObject.SetActive(true);
        UpdateLaserDistance();
        return true;
    }

    public void UpdateLaserDistance()
    {
        RaycastHit l_RaycastHit;
        Ray l_Ray = new Ray(m_Laser.transform.position, m_Laser.transform.forward);
        float l_Distance = m_MaxDistance;
        RefractionCube l_RefractionCube = m_RefractionCubeHit;
        m_RefractionCubeHit = null;

        if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_LayerLaserMask))
        {
            l_Distance = l_RaycastHit.distance;

            if (l_RaycastHit.collider.tag == "RefractionCube")
            {
                m_RefractionCubeHit = l_RaycastHit.collider.GetComponent<RefractionCube>();
                if (!m_RefractionCubeHit.Reflection(gameObject))
                    m_RefractionCubeHit = null;
            }
        }

        m_Laser.SetPosition(1, new Vector3(0f, 0f, l_Distance));
        if (l_RefractionCube != null && l_RefractionCube != m_RefractionCubeHit)
        {
            l_RefractionCube.StopReflection();
        }
    }

    public void StopReflection()
    {
        m_Laser.gameObject.SetActive(false);
        if (m_RefractionCubeHit != null)
            m_RefractionCubeHit.StopReflection();
        m_RefractionCubeHit = null;
    }
}
