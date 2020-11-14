using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : MonoBehaviour
{
    public GameObject m_Button;
    public GameObject m_FirstDoor;

    public AudioSource m_ButtonDown;
    public AudioSource m_ButtonUp;
    public int m_Id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<FPSController>() || other.tag == "Companion")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            m_FirstDoor.transform.position = new Vector3(m_FirstDoor.transform.position.x, m_FirstDoor.transform.position.y + 2.3f, m_FirstDoor.transform.position.z);
            m_ButtonDown.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<FPSController>() || other.tag == "Companion")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            m_FirstDoor.transform.position = new Vector3(m_FirstDoor.transform.position.x, m_FirstDoor.transform.position.y - 2.3f, m_FirstDoor.transform.position.z);
            m_ButtonUp.Play();
        }
    }
}