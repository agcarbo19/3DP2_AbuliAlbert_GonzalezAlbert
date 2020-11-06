using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionSpawner : MonoBehaviour
{
    public GameObject m_CompanionPrefab;
    public Transform m_SpawnerPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        GameObject l_Companion = GameObject.Instantiate(m_CompanionPrefab);
        l_Companion.transform.position = m_SpawnerPosition.position;
        l_Companion.transform.rotation = m_SpawnerPosition.rotation;
    }
}
