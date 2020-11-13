﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLava : MonoBehaviour
{
    public float m_LavaSpeed = 0.01f;
    private Renderer m_renderer;
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float l_Movement = Time.time * m_LavaSpeed;
        m_renderer.material.SetTextureOffset("_MainTex", new Vector2(l_Movement, l_Movement));
    }
}
