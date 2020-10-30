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
    public GameObject m_DummyPortal;

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
        if (Input.GetMouseButtonDown(0))
        {         
                Shoot(m_BluePortal);      
        }

        if (Input.GetMouseButtonDown(1))
        {
            Shoot(m_OrangePortal);
        }
    }

    private void Shoot(Portal _Portal)
    {
        //m_Animator.SetTrigger("IsShooting");
        Ray l_Ray = m_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit l_RaycastHit;
        //m_ShootSound.Play();
        //m_WeaponFlash.Play();

        if (Physics.Raycast(l_Ray, out l_RaycastHit, m_MaxDistance, m_ShootLayerMask.value))
        {
            //DroneEnemy target = l_RaycastHit.transform.GetComponent<DroneEnemy>();
            //DummyTarget dummy = l_RaycastHit.transform.GetComponent<DummyTarget>();

            //if (target != null)
            //{
            //    target.TakeDamage(m_Damage);
            //}
            //else if (dummy != null)
            //{
            //    dummy.m_isHit = true;
            //}

            //if (l_RaycastHit.rigidbody != null)
            //{
            //    l_RaycastHit.rigidbody.AddForce(-l_RaycastHit.normal * m_ImpactForce);
            //}

            //CreateShootHitParticle(l_RaycastHit.point, l_RaycastHit.normal, target != null, l_RaycastHit.transform.tag == "Terrain");
            _Portal.gameObject.SetActive(true);
            bool l_ValidPosition = _Portal.IsValidPosition(l_RaycastHit.point, l_RaycastHit.normal);
            Debug.Log("l_ValidPosition " + l_ValidPosition);
            if (!l_ValidPosition)
            {
                _Portal.gameObject.SetActive(false);
                m_DummyPortal.transform.position = l_RaycastHit.point;
                m_DummyPortal.transform.rotation = Quaternion.LookRotation(l_RaycastHit.normal);
                m_DummyPortal.SetActive(true);
            }
        }

        m_ActualBulletsInMag--;

    }

    IEnumerator Reloading()
    {
        //m_Animator.SetBool("IsReloading", true);
        yield return new WaitForSeconds(1.5f);
        //m_Animator.SetBool("IsReloading", false);
        int l_nAmmo = m_MaxMagSize - m_ActualBulletsInMag;
        if (l_nAmmo > m_GameController.m_Player.GetAmmo())
        {
            m_ActualBulletsInMag += m_GameController.m_Player.GetAmmo();
            m_GameController.m_Player.RemoveAmmo(m_GameController.m_Player.GetAmmo());
        }
        else
        {
            m_ActualBulletsInMag += l_nAmmo;
            m_GameController.m_Player.RemoveAmmo(l_nAmmo);
        }

    }

    private void CreateShootHitParticle(Vector3 Position, Vector3 Normal, bool target, bool terrain)
    {
        //if (target)
        //{
        //    GameObject.Instantiate(m_ImpactEffect, Position, Quaternion.LookRotation(Normal) * Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.value * 180.0f), m_GameController.m_DestroyObjects);
        //}
        //else
        //{
        //    GameObject.Instantiate(m_HitCollisionParticlesPrefab, Position, Quaternion.LookRotation(Normal) * Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.value * 180.0f), m_GameController.m_DestroyObjects);
        //    if (terrain == false)
        //    {
        //        GameObject.Instantiate(m_ImpactEffect, Position, Quaternion.LookRotation(Normal) * Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.value * 180.0f), m_GameController.m_DestroyObjects);
        //    }
        //    else
        //    {
        //        GameObject.Instantiate(m_SmokeImpact, Position, Quaternion.LookRotation(Normal) * Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.value * 180.0f), m_GameController.m_DestroyObjects);
        //    }
        //}
    }
    public int GetBullets()
    {
        return m_ActualBulletsInMag;
    }
}
