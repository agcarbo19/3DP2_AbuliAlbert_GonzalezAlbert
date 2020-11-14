using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform target;
    public float m_CurrentSpeed = 0f;
    public float m_speed = 3f;
    public Animator m_DoorAnimator;
    private Vector3 start, end;

    private void Start()
    {
        m_CurrentSpeed = 0f;
        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
    }

    private void Update()
    {
        if (m_DoorAnimator.GetBool("DoorIsOpen"))
        {
            m_CurrentSpeed = m_speed;
        }
        else
        {
            m_CurrentSpeed = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            float fixedSpeed = m_CurrentSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
        }

        if (transform.position == target.position)
        {
            target.position = (target.position == start) ? end : start;
        }
    }
}