using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Controllers")]
    public FPSController m_Player;
    public WeaponController m_Weapon;


    [Header("Portals")]
    public GameObject m_BluePortal;
    public GameObject m_OrangePortal;
    public bool m_BluePortalActive = false;
    public bool m_OrangePortalActive = false;

    [Header("Crosshair")]
    public List<Sprite> m_CrosshairListSprites;
    public Image m_Crosshair;
    //public List<DroneEnemy> m_Enemies;
    //public Transform m_DestroyObjects;
    //public AudioSource m_Music;

    private void Start()
    {
        //m_Enemies.Add(FindObjectOfType<DroneEnemy>());
        if (m_CrosshairListSprites.Count > 0)
            m_Crosshair.sprite = m_CrosshairListSprites[0];

    }

    private void Update()
    {
        m_BluePortalActive = m_BluePortal.activeSelf;
        m_OrangePortalActive = m_OrangePortal.activeSelf;
        //if (m_GameOverCanv.activeSelf == true)
        //{
        //    if (Input.GetKey(m_JumpKey))
        //    {
        //        Retry();
        //    }
        //}

        #region HUD Crosshair
        if (!m_BluePortalActive && !m_OrangePortalActive)
        {
            m_Crosshair.sprite = m_CrosshairListSprites[0];
        }
        else if (m_BluePortalActive && !m_OrangePortalActive)
        {
            m_Crosshair.sprite = m_CrosshairListSprites[1];
        }
        else if (!m_BluePortalActive && m_OrangePortalActive)
        {
            m_Crosshair.sprite = m_CrosshairListSprites[2];
        }
        else if (m_BluePortalActive && m_OrangePortalActive)
        {
            m_Crosshair.sprite = m_CrosshairListSprites[3];
        }
        #endregion
    }

    public IEnumerator RestartGame(Transform RespawnPoint)
    {
        m_Player.transform.position = RespawnPoint.position;
        yield return new WaitForSeconds(.5f);
        //m_Player.RePatchPlayer();
        //m_Music.Play();

    }

    public void GameOver()
    {
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        //StartCoroutine(m_GameController.RestartGame(m_RespawnPoint));
        //m_GameOverCanv.SetActive(false);

    }


}
