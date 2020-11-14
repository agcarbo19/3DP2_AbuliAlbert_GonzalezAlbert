using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public List<GameObject> m_Buttons;
    public AudioSource m_ButtonDown;
    public AudioSource m_ButtonUp;
    public int m_Id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<FPSController>() || other.tag == "Companion")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            m_ButtonDown.Play();

            foreach (GameObject _Button in m_Buttons)
            {
                if (_Button.tag == "ButtonCompanion")
                {
                    GameEvents.m_Current.ButtonSpawnCompanion(m_Id);
                }
                else if (_Button.tag == "Button")
                {
                    GameEvents.m_Current.ButtonOpenDoor(m_Id);
                }
            }
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
