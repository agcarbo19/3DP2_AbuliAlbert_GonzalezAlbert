using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float m_Damage = 10.0f;
    public float m_FireRate = 15.0f;
    public float m_ImpactForce = 30.0f;
    public float m_NextTimeToFire = 0.0f;
    public int m_MaxMagSize = 30;
    public int m_ActualBulletsInMag;

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
    public Portal m_BluePortal;
    public Portal m_OrangePortal;
    public GameObject m_BlueDummy;
    public GameObject m_OrangeDummy;
    public LayerMask m_PortalsLayerMask;

    //[Header("Weapon Animation")]
    ////public Animator m_Animator;

    //[Header("Reloading")]
    ////public KeyCode m_ReloadKey;

    //[Header("Sounds")]
    //public AudioSource m_ShootSound;

    private void Start()
    {
        m_ActualBulletsInMag = m_MaxMagSize;
        m_BluePortal.gameObject.SetActive(false);
        m_OrangePortal.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_GameController.m_Player.m_ObjectAttached == null)
            Shoot(m_BluePortal);

        if (Input.GetMouseButtonDown(1) && m_GameController.m_Player.m_ObjectAttached == null)
            Shoot(m_OrangePortal);

    }

    private void Shoot(Portal _Portal)
    {
        //m_Animator.SetTrigger("IsShooting");
        Ray l_Ray = m_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RaycastHit;
        bool l_ValidPosition = true;
        //m_ShootSound.Play();
        //m_WeaponFlash.Play();

        //Comprova si el RaycastHit ja hi ha un portal.
        if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_PortalsLayerMask.value))
        {
            if (l_RaycastHit.transform.name != _Portal.transform.name)
            {
                l_ValidPosition = false;
            }
        }
        else if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_ShootLayerMask.value))
        {
            _Portal.gameObject.SetActive(true);
            l_ValidPosition = _Portal.IsValidPosition(l_RaycastHit.point, l_RaycastHit.normal);
            //Debug.Log("l_ValidPosition " + l_ValidPosition);           
        }
        if (!l_ValidPosition)
        {
            _Portal.gameObject.SetActive(false);

            if (_Portal.name == "OrangePortal")
            {
                //m_OrangeDummy.transform.position = l_RaycastHit.point;
                //m_OrangeDummy.transform.rotation = Quaternion.LookRotation(l_RaycastHit.normal);
                ////m_OrangeDummy.SetActive(true);
                Instantiate(m_OrangeDummy, l_RaycastHit.point, Quaternion.LookRotation(l_RaycastHit.normal));
            }
            if (_Portal.name == "BluePortal")
            {
                //m_BlueDummy.transform.position = l_RaycastHit.point;
                //m_BlueDummy.transform.rotation = Quaternion.LookRotation(l_RaycastHit.normal);
                //m_BlueDummy.SetActive(true);
                Instantiate(m_BlueDummy, l_RaycastHit.point, Quaternion.LookRotation(l_RaycastHit.normal));
            }

        }

    }
}
