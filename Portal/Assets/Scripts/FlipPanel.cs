using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FlipPanel : ButtonAction
{
    private Vector3 m_PanelFlip = new Vector3(0f, 180f, 0f);
    private Vector3 m_CurrentAngle;
    private Vector3 m_InitialAngle;

    private void Awake()
    {
        m_CurrentAngle = transform.eulerAngles;
        m_InitialAngle = transform.eulerAngles;
    }

    private void Update()
    {
        if (m_InAction)
        {
            m_CurrentAngle = new Vector3(
             Mathf.LerpAngle(m_CurrentAngle.x, m_PanelFlip.x, Time.deltaTime),
             Mathf.LerpAngle(m_CurrentAngle.y, m_PanelFlip.y, Time.deltaTime),
             Mathf.LerpAngle(m_CurrentAngle.z, m_PanelFlip.z, Time.deltaTime));

            transform.eulerAngles = m_CurrentAngle;
        }
        else
        {
            m_CurrentAngle = new Vector3(
             Mathf.LerpAngle(m_CurrentAngle.x, m_InitialAngle.x, Time.deltaTime),
             Mathf.LerpAngle(m_CurrentAngle.y, m_InitialAngle.y, Time.deltaTime),
             Mathf.LerpAngle(m_CurrentAngle.z, m_InitialAngle.z, Time.deltaTime));

            transform.eulerAngles = m_CurrentAngle;
        }
    }
}
