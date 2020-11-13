using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gum_Material : MonoBehaviour
{
    public float m_Force = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Companion")
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(0f,15f,0f), ForceMode.Impulse);
        }
    }

}
