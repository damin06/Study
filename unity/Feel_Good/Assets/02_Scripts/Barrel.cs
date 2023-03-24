using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private Texture[] textures;
    private MeshRenderer render;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
        int idx = Random.Range(0, textures.Length);
        render.material.mainTexture = textures[idx];
    }
}
