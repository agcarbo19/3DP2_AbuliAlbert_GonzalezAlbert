using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionSpawner : MonoBehaviour
{
    public GameObject m_CompanionPrefab;
    public int m_Id;

    private void Start()
    {
        GameEvents.m_Current.OnButtonSpawnCompanion += Spawn;
    }

    public void Spawn(int _id)
    {
        if (_id == m_Id)
        {
        GameObject l_Companion = GameObject.Instantiate(m_CompanionPrefab);
        l_Companion.transform.position = transform.position;
        l_Companion.transform.rotation = transform.rotation;
        }
    }
}
