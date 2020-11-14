using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDualDoor : MonoBehaviour
{
    public ButtonOpenDualDoor m_Button1;
    public ButtonOpenDualDoor m_Button2;

    private void Update()
    {
            GameEvents.m_Current.ButtonsOpenDoor(m_Button1.m_ReturnedId, m_Button2.m_ReturnedId);
    }


}
