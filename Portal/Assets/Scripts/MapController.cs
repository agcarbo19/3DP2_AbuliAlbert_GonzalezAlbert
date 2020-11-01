using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> m_Walls;
    public Texture m_DrawableWallTex;
    public Texture m_NonDrawableWallTex;

    private void Start()
    {
        foreach (GameObject _Wall in m_Walls)
        {
            if (_Wall.tag == "Drawable")
            {
                _Wall.GetComponent<MeshRenderer>().material.mainTexture = m_DrawableWallTex;
            }
            else
            {
                _Wall.GetComponent<MeshRenderer>().material.mainTexture = m_NonDrawableWallTex;
            }
        }
    }
}
