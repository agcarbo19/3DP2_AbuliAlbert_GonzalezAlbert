using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionSpawner : ButtonAction
{
    public GameObject m_CompanionPrefab;

    private void Update()
    {
        if (m_InAction)
        {
            Spawn();
        }

    }

    public void Spawn()
    {
        GameObject l_Companion = GameObject.Instantiate(m_CompanionPrefab);
        l_Companion.transform.position = transform.position;
        l_Companion.transform.rotation = transform.rotation;
        m_InAction = false;
    }
}
