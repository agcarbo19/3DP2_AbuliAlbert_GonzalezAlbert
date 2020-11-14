using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public GameObject m_door;
    public int m_doorTimer = 10;
    public List<int> m_ids;

    bool m_isRoutine = false;
    public bool m_isOpen = false;

    private void Start()
    {
        GameEvents.m_Current.OnButtonOpenDoor += OpenDoor;
    }

    public void OpenDoor(int _id)
    {
        foreach (int m_id in m_ids)
        {
            if (_id == m_id)
            {
                if (m_isOpen == false)
                {
                    if (m_isRoutine == false)
                    {
                        gameObject.GetComponent<Animation>().Play("rightDoorOpen");
                        gameObject.GetComponent<Animation>().Play("leftDoorOpen");

                        StartCoroutine(DoorTimer());
                        m_isOpen = true;
                        m_isRoutine = true;
                    }
                }
            }
        }
    }

    private IEnumerator DoorTimer()
    {
        yield return new WaitForSeconds(m_doorTimer);

        gameObject.GetComponent<Animation>().Play("rightDoorClose");
        gameObject.GetComponent<Animation>().Play("leftDoorClose");
        m_isOpen = false;
        m_isRoutine = false;
    }

    private void OnDestroy()
    {
        GameEvents.m_Current.OnButtonOpenDoor -= OpenDoor;
    }
}