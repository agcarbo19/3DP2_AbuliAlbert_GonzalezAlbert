using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal m_MirrorPortal;
    public Transform m_MirrorPortalTransform;
    public Camera m_Camera;
    public FPSController m_Player;
    public float m_CameraOffset = 0.6f;
    void Start()
    {

    }

    void Update()
    {
        Vector3 l_LocalPosition = m_MirrorPortal.m_MirrorPortalTransform.InverseTransformPoint(m_Player.m_Camera.transform.position);
        m_Camera.transform.position = transform.TransformPoint(l_LocalPosition);
        Vector3 l_LocalDirection = m_MirrorPortal.m_MirrorPortalTransform.InverseTransformDirection(m_Player.m_Camera.transform.forward);
        m_Camera.transform.forward = transform.TransformDirection(l_LocalDirection);

        float l_DistanceToPortal = Vector3.Distance(m_Camera.transform.position, transform.position);
        m_Camera.nearClipPlane = l_DistanceToPortal + m_CameraOffset;
    }
}
