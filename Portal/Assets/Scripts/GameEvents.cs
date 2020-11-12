using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents m_Current;

    private void Awake()
    {
        m_Current = this;
    }

    public event Action OnButtonOpenDoor;
    public void ButtonOpenDoor()
    {
        if (OnButtonOpenDoor != null)
            OnButtonOpenDoor();
    }

    public event Action<int> OnButtonSpawnCompanion;
    public void ButtonSpawnCompanion(int _id)
    {
        if (OnButtonSpawnCompanion != null)
            OnButtonSpawnCompanion(_id);
    }

}
