using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsOnTime : MonoBehaviour
{
    public float m_DestroyOnTime = 3.0f;
    void Start()
    {
        StartCoroutine(DestroyObjectOnTimeFn());
    }
    IEnumerator DestroyObjectOnTimeFn()
    {
        yield return new WaitForSeconds(m_DestroyOnTime);
        GameObject.Destroy(gameObject);
    }

}
