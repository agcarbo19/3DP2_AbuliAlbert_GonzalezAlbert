using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Remoting.Messaging;

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

    [Header("GameOver")]
    public GameObject m_GameOverPanel;

    private void Start()
    {
        if (m_CrosshairListSprites.Count > 0)
            m_Crosshair.sprite = m_CrosshairListSprites[0];
        m_GameOverPanel.SetActive(false);
     
    }

    private void Update()
    {
        m_BluePortalActive = m_BluePortal.activeSelf;
        m_OrangePortalActive = m_OrangePortal.activeSelf;
        if (m_GameOverPanel.activeSelf == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Retry();
            }
        }

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

    public void RestartGame(Transform RespawnPoint)
    {
        m_Player.m_CharacterController.enabled = false;
        m_Player.RePatchPlayer();
        m_Player.transform.position = RespawnPoint.position;
        m_Player.transform.rotation = RespawnPoint.rotation;
        m_Player.m_CharacterController.enabled = true;
        //m_Music.Play();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        m_GameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        m_GameOverPanel.SetActive(false);
        RestartGame(m_Player.m_RespawnPoint);
    }


}
