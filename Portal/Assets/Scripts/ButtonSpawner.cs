using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public AudioSource m_ButtonDown;
    public AudioSource m_ButtonUp;
    public int m_Id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<FPSController>() || other.tag == "Companion")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            m_ButtonDown.Play();
            GameEvents.m_Current.ButtonSpawnCompanion(m_Id);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<FPSController>() || other.tag == "Companion")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
            m_ButtonUp.Play();
        }
    }

}
