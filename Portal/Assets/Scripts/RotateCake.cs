using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCake : MonoBehaviour
{
    public float m_RotationSpeed=5f;
    void Update()
    {
        transform.Rotate(Vector3.up * (m_RotationSpeed * Time.deltaTime));
    }
}
