using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int m_Id1;
    public float m_timer = 0f;
    public float m_TimeUntilClose = 20f;
    public bool m_TimeToClose = false;
    public Animator m_Animator;

    private void Start()
    {
        GameEvents.m_Current.OnButtonOpenDoor += OpenDoor;
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public void OpenDoor(int _id1)
    {
        if (_id1 == m_Id1)
        {
            m_Animator.SetBool("DoorIsOpen", true);
            m_TimeToClose = true;
        }

    }

    private void Update()
    {
        if (m_TimeToClose == true)
        {
            m_timer += Time.deltaTime;
        }
        else
        {
            m_timer = 0f;
        }

        if (m_timer >= m_TimeUntilClose)
        {
            m_Animator.SetBool("DoorIsOpen", false);
            m_TimeToClose = false;
        }

    }

    private void OnDestroy()
    {
        GameEvents.m_Current.OnButtonOpenDoor -= OpenDoor;
    }
}
