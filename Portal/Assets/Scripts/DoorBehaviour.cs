using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    //public GameObject m_door;
    //public int m_doorTimer = 10;
    //public List<int> m_ids;

    //bool m_isRoutine = false;
    //public bool m_isOpen = false;

    //private void Start()
    //{
    //    GameEvents.m_Current.OnButtonOpenDoor += OpenDoor;
    //}

    //public void OpenDoor(int _id)
    //{
    //    foreach (int m_id in m_ids)
    //    {
    //        if (_id == m_id)
    //        {
    //            if (m_isOpen == false)
    //            {
    //                if (m_isRoutine == false)
    //                {
    //                    gameObject.GetComponent<Animation>().Play("DoorOpen");
    //                    //gameObject.GetComponent<Animation>().Play("leftDoorOpen");

    //                    StartCoroutine(DoorTimer());
    //                    m_isOpen = true;
    //                    m_isRoutine = true;
    //                }
    //            }
    //        }
    //    }
    //}

    //private IEnumerator DoorTimer()
    //{
    //    yield return new WaitForSeconds(m_doorTimer);

    //    gameObject.GetComponent<Animation>().Play("DoorClose");
    //    //gameObject.GetComponent<Animation>().Play("leftDoorClose");
    //    m_isOpen = false;
    //    m_isRoutine = false;
    //}

    //private void OnDestroy()
    //{
    //    GameEvents.m_Current.OnButtonOpenDoor -= OpenDoor;
    //}

    public int m_Id1;
    public int m_Id2;
    public float m_timer = 0f;
    public bool m_TimeToClose = false;
    public Animator m_Animator;

    private void Start()
    {
        GameEvents.m_Current.OnButtonsOpenDoor += OpenDoor;
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public void OpenDoor(int _id1, int _id2)
    {
        if (_id1 == m_Id1 && _id2 == m_Id2)
        {
            m_Animator.SetBool("DoorIsOpen", true);
        }
        else
        {
            m_TimeToClose = true;
        }

    }

    private void Update()
    {
        if (m_TimeToClose == true)
        {
            m_timer += Time.deltaTime;
        }
        else
        {
            m_timer = 0f;
        }

        if (m_timer >= 5f)
        {
            m_Animator.SetBool("DoorIsOpen", false);
            m_TimeToClose = false;
        }

    }

    private void OnDestroy()
    {
        GameEvents.m_Current.OnButtonsOpenDoor -= OpenDoor;
    }
}