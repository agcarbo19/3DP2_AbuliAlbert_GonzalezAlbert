using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameController m_GameController;
    public GameObject m_ValidPointsObject;
    public List<Transform> m_ValidPoints;
    public float m_MinDistanceToValidPoint = 1.01f;
    public float m_MaxDistanceToValidPoint = 1.31f;
    public float m_MinDot = 0.9f;

    public bool IsValidPosition(Vector3 Position, Vector3 Normal)
    {
        m_ValidPointsObject.transform.position = Position;
        m_ValidPointsObject.transform.rotation = Quaternion.LookRotation(Normal);
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
