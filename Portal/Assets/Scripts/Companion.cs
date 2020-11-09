using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Companion : MonoBehaviour
{
    private bool m_Teleportable = true;
    private Rigidbody m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            Teleport(other.GetComponent<SubPortal>());
        }
    }

    private void Update()
    {
        gameObject.GetComponent<BoxCollider>().enabled = !m_Rigidbody.isKinematic;
    }

    void Teleport(SubPortal _Portal)
    {
        m_Rigidbody.isKinematic = true;
        Vector3 l_LocalVelocity = _Portal.m_MirrorPortalTransform.InverseTransformDirection(m_Rigidbody.velocity);
        
        Vector3 l_LocalPosition = _Portal.transform.InverseTransformPoint(transform.position);
        transform.position = _Portal.m_MirrorPortal.transform.TransformPoint(l_LocalPosition);

        Vector3 l_LocalDirection = _Portal.transform.InverseTransformDirection(-transform.forward);
        transform.forward = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirection);

        float l_ScaleFactor = _Portal.m_MirrorPortal.transform.localScale.x / _Portal.transform.localScale.x;
        transform.localScale = _Portal.m_MirrorPortal.transform.localScale;

        m_Rigidbody.velocity = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalVelocity);

        m_Rigidbody.isKinematic = false;

    }

    public void SetTeleportable(bool _Teleportable)
    {
        m_Teleportable = _Teleportable;
    }
}
