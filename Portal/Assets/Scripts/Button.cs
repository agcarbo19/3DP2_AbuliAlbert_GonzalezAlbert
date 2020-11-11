using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public AudioSource m_ButtonDown;
    public AudioSource m_ButtonUp;
    public GameObject m_ActionGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<FPSController>() || other.tag == "Companion")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            m_ButtonDown.Play();
            m_ActionGameObject.GetComponent<FlipPanel>().m_InAction = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<FPSController>() || other.tag == "Companion")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            m_ButtonUp.Play();
            m_ActionGameObject.GetComponent<FlipPanel>().m_InAction = false;
        }
    }

}
