using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunken : MonoBehaviour
{
    public Material material;

    void OnRenderImage (RenderTexture source, RenderTexture destination) 
    {
        Debug.Log("Drunken renderer");
        Graphics.Blit (source, destination, material);
    }
}
