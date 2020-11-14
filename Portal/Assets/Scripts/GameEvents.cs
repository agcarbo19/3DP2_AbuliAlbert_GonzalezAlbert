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

    public event Action<int> OnButtonOpenDoor;
    public void ButtonOpenDoor(int _id)
    {
        if (OnButtonOpenDoor != null)
            OnButtonOpenDoor(_id);
    }
    
    public event Action<int, int> OnButtonsOpenDoor;
    public void ButtonsOpenDoor(int _id1, int _id2)
    {
        if (OnButtonsOpenDoor != null)
            OnButtonsOpenDoor(_id1, _id2);
    }

    public event Action<int> OnButtonSpawnCompanion;
    public void ButtonSpawnCompanion(int _id)
    {
        if (OnButtonSpawnCompanion != null)
            OnButtonSpawnCompanion(_id);
    }

    public event Action<int> OnButtonActivatePlatform;
    public void ButtonActivatePlatform(int _id)
    {
        if (OnButtonActivatePlatform != null)
            OnButtonActivatePlatform(_id);
    }
}
