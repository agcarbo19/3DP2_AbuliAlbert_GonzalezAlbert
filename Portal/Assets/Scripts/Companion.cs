using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    bool m_Teleportable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            Teleport(other.GetComponent<Portal>());
        }
    }

    void Teleport(Portal _Portal)
    {
        Rigidbody l_Rigidbody = GetComponent<Rigidbody>();
        Vector3 l_LocalVelocity = _Portal.m_MirrorPortalTransform.InverseTransformDirection(l_Rigidbody.velocity);
        l_Rigidbody.isKinematic = true;
        
        Vector3 l_LocalPosition = _Portal.transform.InverseTransformPoint(transform.position);
        transform.position = _Portal.m_MirrorPortal.transform.TransformPoint(l_LocalPosition);

        Vector3 l_LocalDirection = _Portal.transform.InverseTransformDirection(-transform.forward);
        transform.forward = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirection);

        float l_ScaleFactor = _Portal.m_MirrorPortal.transform.localScale.x / _Portal.transform.localScale.x;
        transform.localScale = transform.localScale * l_ScaleFactor;

        l_Rigidbody.isKinematic = false;
        l_Rigidbody.velocity = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalVelocity);
    }

    public void SetTeleportable(bool _Teleportable)
    {
        m_Teleportable = _Teleportable;
    }
}
