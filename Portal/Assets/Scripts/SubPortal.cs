using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPortal : MonoBehaviour
{
    public GameController m_GameController;
    public GameObject m_CylinderPortal;
    public SubPortal m_MirrorPortal;
    public Transform m_MirrorPortalTransform;
    public Camera m_Camera;
    public float m_CameraOffset = 0.6f;
    public Material m_CameraRTMaterial;
    public AnimationClip m_OpenPortal;
    private Animation m_Animation;

    private void Awake()
    {
        m_Animation = gameObject.GetComponent<Animation>();
        m_GameController = FindObjectOfType<GameController>();
    }
    private void Start()
    {
        //Resolució de les cameras adaptativa
        if (m_Camera.targetTexture != null)
        {
            m_Camera.targetTexture.Release();
        }
        m_Camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        m_CameraRTMaterial.mainTexture = m_Camera.targetTexture;
    }

    void Update()
    {
        Vector3 l_LocalPosition = m_MirrorPortal.m_MirrorPortalTransform.InverseTransformPoint(m_GameController.m_Player.m_Camera.transform.position);
        m_Camera.transform.position = transform.TransformPoint(l_LocalPosition);
        Vector3 l_LocalDirection = m_MirrorPortal.m_MirrorPortalTransform.InverseTransformDirection(m_GameController.m_Player.m_Camera.transform.forward);
        m_Camera.transform.forward = transform.TransformDirection(l_LocalDirection);

        float l_DistanceToPortal = Vector3.Distance(m_Camera.transform.position, transform.position);
        m_Camera.nearClipPlane = l_DistanceToPortal + m_CameraOffset;

        if (!m_GameController.m_BluePortalActive || !m_GameController.m_OrangePortalActive)
        {
            m_CylinderPortal.SetActive(false);
        }
        else
        {
            m_CylinderPortal.SetActive(true);
        }
    }

    private void OnEnable()
    {
        //m_Animation.clip = m_OpenPortal;
        //m_Animation.Play();
    }
}
