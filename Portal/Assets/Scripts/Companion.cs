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
            gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(Teleport(other.GetComponent<SubPortal>()));
        }
    }

    IEnumerator Teleport(SubPortal _Portal)
    {
        Vector3 l_LocalVelocity = _Portal.m_MirrorPortalTransform.InverseTransformDirection(m_Rigidbody.velocity);
        
        Vector3 l_LocalPosition = _Portal.transform.InverseTransformPoint(transform.position);
        transform.position = _Portal.m_MirrorPortal.transform.TransformPoint(l_LocalPosition);

        Vector3 l_LocalDirection = _Portal.transform.InverseTransformDirection(-transform.forward);
        transform.forward = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalDirection);

        float l_ScaleFactor = _Portal.m_MirrorPortal.transform.localScale.x / _Portal.transform.localScale.x;
        transform.localScale = _Portal.m_MirrorPortal.transform.localScale;

        m_Rigidbody.velocity = _Portal.m_MirrorPortal.transform.TransformDirection(l_LocalVelocity);

        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void SetTeleportable(bool _Teleportable)
    {
        m_Teleportable = _Teleportable;
    }
}
