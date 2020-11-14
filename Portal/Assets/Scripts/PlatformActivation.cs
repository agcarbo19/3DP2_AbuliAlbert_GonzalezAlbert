using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivation : MonoBehaviour
{
    public GameObject m_Platform;
    public MovingPlatform m_movingPlatform;
    public DoorBehaviour m_doorBehaviour;

    public bool m_isActive;

    void Start()
    {
        m_Platform.gameObject.SetActive(false);
        m_isActive = false;

        //GameEvents.m_Current.OnButtonActivatePlatform += ActivatePlatform;
    }

    //public void ActivatePlatform(int _id)
    //{
    //    foreach (int m_id in m_doorBehaviour.m_ids)
    //    {
    //        if (m_movingPlatform.m_id == m_id)
    //        {
    //            if (m_doorBehaviour.m_isOpen == true)
    //            {
    //                m_isActive = true;

    //                if (m_isActive == true)
    //                {
    //                    m_Platform.gameObject.SetActive(true);
    //                }
    //            }
    //        }
    //    }
    //}
}