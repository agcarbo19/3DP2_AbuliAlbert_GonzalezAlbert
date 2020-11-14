using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenDualDoor : MonoBehaviour
{
    public AudioSource m_ButtonDown;
    public AudioSource m_ButtonUp;
    public int m_Id;
    public int m_ReturnedId = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<FPSController>() || other.tag == "Companion")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            m_ButtonDown.Play();
            m_ReturnedId = m_Id;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<FPSController>() || other.tag == "Companion")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            m_ButtonUp.Play();
            m_ReturnedId = 0;
        }
    }

}
