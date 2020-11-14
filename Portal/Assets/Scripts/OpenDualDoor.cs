using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDualDoor : MonoBehaviour
{
    public ButtonOpenDoor m_Button1;
    public ButtonOpenDoor m_Button2;

    private void Update()
    {
            GameEvents.m_Current.ButtonsOpenDoor(m_Button1.m_ReturnedId, m_Button2.m_ReturnedId);
    }


}
