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
    public Vector3 m_PortalToPlayer = new Vector3(0f, 0f, 0f);
    public LineRenderer m_Laser;
    public LayerMask m_LaserLayerMask;
    public RefractionCube m_RefractionCubeHit;
    public float m_MaxDistance = 250f;

    private void Awake()
    {
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
        m_Laser.gameObject.SetActive(false);
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
        m_PortalToPlayer = gameObject.transform.position - m_GameController.m_Player.transform.position;

        if (m_Laser.gameObject.activeSelf)
            UpdateLaserDistance();
    }

    public void UpdateLaserDistance()
    {
        RefractionCube l_RefractionCube = m_RefractionCubeHit;
        RaycastHit l_RaycastHit;
        Ray l_Ray = new Ray(m_Laser.transform.position, m_Laser.transform.forward);
        float l_Distance = m_MaxDistance;
        m_RefractionCubeHit = null;

        if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_LaserLayerMask))
        {
            l_Distance = l_RaycastHit.distance;
            if (l_RaycastHit.collider.tag == "RefractionCube")
            {
                m_RefractionCubeHit = l_RaycastHit.collider.GetComponent<RefractionCube>();
                if (m_RefractionCubeHit.Reflection(gameObject))
                    m_RefractionCubeHit = null;
            }
        }

        m_Laser.SetPosition(1, new Vector3(0f, 0f, l_Distance));
        if (l_RefractionCube != m_RefractionCubeHit)
            l_RefractionCube.StopReflection();
    }

    public void Reflection(Vector3 _Position, Vector3 _Direction)
    {
        //Debug.DrawLine(_Position, transform.forward * 5f, Color.red);
        Vector2 l_LocalPosition = m_MirrorPortalTransform.InverseTransformPoint(_Position);
        m_MirrorPortal.m_Laser.transform.localPosition = l_LocalPosition;

        Vector3 l_LocalDirection = m_MirrorPortalTransform.InverseTransformDirection(_Direction);
        m_MirrorPortal.m_Laser.transform.localRotation = Quaternion.LookRotation(l_LocalDirection);

        m_MirrorPortal.m_Laser.gameObject.SetActive(true);
        UpdateLaserDistance();
    }

}
