using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPortal : MonoBehaviour
{
    public List<Transform> m_ValidPoints;
    public GameController m_GameController;
    public float m_MinDistanceToValidPoint = 1.01f;
    public float m_MaxDistanceToValidPoint = 1.31f;
    public float m_MinDot = 0.9f;

    public LayerMask m_ShootLayerMask;
    public float m_MaxDistance;
    public LayerMask m_PortalsLayerMask;

    private void Awake()
    {
        m_MaxDistance = m_GameController.m_Weapon.m_MaxDistance;
    }

    public void Update()
    {
        Ray l_Ray = m_GameController.m_Player.m_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RaycastHit;
        bool l_ValidPosition = true;

        ////Comprova si el RaycastHit ja hi ha un portal.
        //if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_PortalsLayerMask.value))
        //{
        //    if (l_RaycastHit.transform.name != _Portal.transform.name)
        //    {
        //        l_ValidPosition = false;
        //    }
        //}
        //else 
        if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_ShootLayerMask.value))
        {
            l_ValidPosition = IsValidPosition(l_RaycastHit.point, l_RaycastHit.normal);
            //Debug.Log("l_ValidPosition " + l_ValidPosition);           
        }
        if (!l_ValidPosition)
        {
            

        }

    }


    public bool IsValidPosition(Vector3 Position, Vector3 Normal)
    {
        transform.position = Position;
        transform.rotation = Quaternion.LookRotation(Normal);
        bool l_IsValid = true;
        RaycastHit l_RaycastHit;

        foreach (Transform l_ValidPoint in m_ValidPoints)
        {
            Vector3 l_Direction = l_ValidPoint.position - m_GameController.m_Player.m_Camera.transform.position;
            Ray l_Ray = new Ray(m_GameController.m_Player.m_Camera.transform.position, l_Direction);

            if (Physics.Raycast(l_Ray, out l_RaycastHit, m_GameController.m_Weapon.m_MaxDistance, m_GameController.m_Weapon.m_ShootLayerMask.value))
            {
                if (l_RaycastHit.collider.tag == "Drawable")
                {
                    float l_DistanceToHitPoint = Vector3.Distance(Position, l_RaycastHit.point);
                    //Debug.Log("Distance to calid position : " + l_DistanceToHitPoint);
                    if (l_DistanceToHitPoint >= m_MinDistanceToValidPoint && l_DistanceToHitPoint <= m_MaxDistanceToValidPoint)
                    {
                        float l_Dot = Vector3.Dot(l_RaycastHit.normal, Normal);
                        //Debug.Log("Dot : " + l_Dot);
                        if (l_Dot < m_MinDot)
                            l_IsValid = false;
                    }
                    else
                        l_IsValid = false;
                }
                else
                    l_IsValid = false;
            }
            else
                l_IsValid = false;
        }
        return l_IsValid;
    }

}
