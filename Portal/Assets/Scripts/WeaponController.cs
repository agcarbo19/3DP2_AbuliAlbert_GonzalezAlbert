using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public Camera m_Camera;
    public GameController m_GameController;
    //public ParticleSystem m_WeaponFlash;
    //public GameObject m_SmokeImpact;
    //public GameObject m_ImpactEffect;
    //public FPSController m_Player;

    [Header("Shoot")]
    public LayerMask m_ShootLayerMask;
    //public GameObject m_HitCollisionParticlesPrefab;
    public float m_MaxDistance = 150.0f;

    [Header("Portals")]
    public Portal m_PreviewPortal;
    public SubPortal m_BluePortal;
    public SubPortal m_OrangePortal;
    public GameObject m_BlueDummy;
    public GameObject m_OrangeDummy;
    public bool m_ValidPosition;
    //public GameObject m_PreviewPointsPortal;

    public LayerMask m_PortalsLayerMask;
    private Color m_PreviewColor;
    private ParticleSystem.MainModule m_MainPreviewParticle;
    public float m_PortalSizeMultiplier = 1.1f;
    public float m_MaxPortalSize = 2f;
    public float m_MinPortalSize = 0.5f;

    //[Header("Weapon Animation")]
    ////public Animator m_Animator;

    //[Header("Reloading")]
    ////public KeyCode m_ReloadKey;

    [Header("Sounds")]
    public AudioSource m_ShootSound;

    private void Start()
    {
        m_BluePortal.gameObject.SetActive(false);
        m_OrangePortal.gameObject.SetActive(false);
        m_PreviewPortal.gameObject.SetActive(false);
        m_MainPreviewParticle = m_PreviewPortal.GetComponentInChildren<ParticleSystem>().main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && m_GameController.m_Player.m_ObjectAttached == null)
            PreviewPortal(m_BluePortal);
        if (Input.GetMouseButtonUp(0) && m_GameController.m_Player.m_ObjectAttached == null)
            Shoot(m_BluePortal);


        if (Input.GetMouseButton(1) && m_GameController.m_Player.m_ObjectAttached == null)
            PreviewPortal(m_OrangePortal);
        if (Input.GetMouseButtonUp(1) && m_GameController.m_Player.m_ObjectAttached == null)
            Shoot(m_OrangePortal);
    }

    private void Shoot(SubPortal _Portal)
    {
        //m_Animator.SetTrigger("IsShooting");
        m_ShootSound.Play();
        //m_WeaponFlash.Play();

        if (!m_ValidPosition)
        {
            _Portal.gameObject.SetActive(false);

            if (_Portal.name == "OrangePortal")
            {
                Instantiate(m_OrangeDummy, m_PreviewPortal.transform.position, m_PreviewPortal.transform.rotation);
            }
            if (_Portal.name == "BluePortal")
            {
                Instantiate(m_BlueDummy, m_PreviewPortal.transform.position, m_PreviewPortal.transform.rotation);
            }
        }
        else
        {
            _Portal.gameObject.SetActive(true);
            _Portal.transform.localScale = m_PreviewPortal.transform.localScale;
            _Portal.GetComponentInChildren<ParticleSystem>().transform.localScale = m_PreviewPortal.GetComponentInChildren<ParticleSystem>().transform.localScale;
            _Portal.transform.position = m_PreviewPortal.transform.position;
            _Portal.transform.rotation = m_PreviewPortal.transform.rotation;
        }
        m_PreviewPortal.gameObject.SetActive(false);
    }
    private void PreviewPortal(SubPortal _Portal)
    {

        if (m_PreviewPortal.gameObject.activeSelf == false)
            m_PreviewPortal.gameObject.SetActive(true);

        Ray l_Ray = m_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RaycastHit;

        //Comprova si el RaycastHit ja hi ha un portal.
        if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_PortalsLayerMask.value))
        {
            if (l_RaycastHit.transform.name != _Portal.transform.name)
            {
                m_ValidPosition = false;
            }
        }
        else
        if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_ShootLayerMask.value))
        {
            m_ValidPosition = m_PreviewPortal.IsValidPosition(l_RaycastHit.point, l_RaycastHit.normal);
        }

        if (!m_ValidPosition)
        {
            m_PreviewColor = Color.red;
            m_PreviewColor.a = 0.1f;

        }
        else
        {
            m_PreviewColor = Color.green;
            m_PreviewColor.a = 0.1f;
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            if (m_PreviewPortal.transform.localScale.x < m_MaxPortalSize)
            {
                m_PreviewPortal.transform.localScale *= m_PortalSizeMultiplier;
                m_PreviewPortal.GetComponentInChildren<ParticleSystem>().transform.localScale *= m_PortalSizeMultiplier;
                m_PreviewPortal.m_MaxDistanceToValidPoint *= m_PortalSizeMultiplier;
                m_PreviewPortal.m_MinDistanceToValidPoint *= m_PortalSizeMultiplier;
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            if (m_PreviewPortal.transform.localScale.x > m_MinPortalSize)
            {
                m_PreviewPortal.transform.localScale /= m_PortalSizeMultiplier;
                m_PreviewPortal.GetComponentInChildren<ParticleSystem>().transform.localScale /= m_PortalSizeMultiplier;
                m_PreviewPortal.m_MaxDistanceToValidPoint /= m_PortalSizeMultiplier;
                m_PreviewPortal.m_MinDistanceToValidPoint /= m_PortalSizeMultiplier;

            }
        }

        m_PreviewPortal.transform.position = l_RaycastHit.point;
        m_PreviewPortal.transform.rotation = Quaternion.LookRotation(l_RaycastHit.normal);
        m_MainPreviewParticle.startColor = m_PreviewColor;

    }
}
