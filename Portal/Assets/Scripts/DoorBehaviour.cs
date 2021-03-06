﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public int m_Id1;
    public int m_Id2;
    public float m_timer = 0f;
    public bool m_TimeToClose = false;
    public Animator m_Animator;

    private void Start()
    {
        GameEvents.m_Current.OnButtonsOpenDoor += OpenDoor;
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public void OpenDoor(int _id1, int _id2)
    {
        if (_id1 == m_Id1 && _id2 == m_Id2)
        {
            m_Animator.SetBool("DoorIsOpen", true);
        }
        else
        {
            m_TimeToClose = true;
        }

    }

    private void Update()
    {
        if (m_TimeToClose)
        {
            m_timer += Time.deltaTime;
        }
        else
        {
            m_timer = 0f;
        }

        if (m_timer >= 5f)
        {
            m_Animator.SetBool("DoorIsOpen", false);
            m_TimeToClose = false;
        }

    }

    private void OnDestroy()
    {
        GameEvents.m_Current.OnButtonsOpenDoor -= OpenDoor;
    }
}