using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal m_MirrorPortal;
    public Transform m_MirrorPortalTransform;
    public Camera m_Camera;
    public GameController m_GameController;
    public float m_CameraOffset = 0.6f;
    public List<Transform> m_ValidPoints;
    public float m_MinDistanceToValidPoint = 0f;
    public float m_MaxDistanceToValidPoint = 500f;
    public float m_MinDot = 0.9f;
    public Material m_CameraRTMaterial;
    public AnimationClip m_OpenPortal;
    private Animation m_Animation;
    public GameObject m_CylinderPortal;
    public GameObject m_ValidPointsObject;

    private void Awake()
    {
        m_Animation = gameObject.GetComponent<Animation>();
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

    public bool IsValidPosition(Vector3 Position, Vector3 Normal)
    {
        m_ValidPointsObject.transform.position = Position;
        m_ValidPointsObject.transform.rotation = Quaternion.LookRotation(Normal);
        bool l_IsValid = true;
        RaycastHit l_RaycastHit;

        foreach (Transform l_ValidPoint in m_ValidPoints)
        {
            Vector3 l_Direction = l_ValidPoint.position - m_GameController.m_Player.m_Camera.transform.position;
            Ray l_Ray = new Ray(m_GameController.m_Player.m_Camera.transform.position, l_Direction);

            if (Physics.Raycast(l_Ray, out l_RaycastHit, m_GameController.m_Weapon.m_MaxDistance, m_GameController.m_Weapon.m_ShootLayerMask.value))
            {
                if (l_RaycastHit.collider.tag == "Drawable")
                {
                    float l_DistanceToHitPoint = Vector3.Distance(Position, l_RaycastHit.point);
                    //Debug.Log("Distance to calid position : " + l_DistanceToHitPoint);
                    if (l_DistanceToHitPoint >= m_MinDistanceToValidPoint && l_DistanceToHitPoint <= m_MaxDistanceToValidPoint)
                    {
                        float l_Dot = Vector3.Dot(l_RaycastHit.normal, Normal);
                        //Debug.Log("Dot : " + l_Dot);
                        if (l_Dot < m_MinDot)
                            l_IsValid = false;
                    }
                    else
                        l_IsValid = false;
                }
                else
                    l_IsValid = false;
            }
            else
                l_IsValid = false;
        }
        return l_IsValid;
    }

    private void OnEnable()
    {
        m_Animation.clip = m_OpenPortal;
        m_Animation.Play();
    }
}
