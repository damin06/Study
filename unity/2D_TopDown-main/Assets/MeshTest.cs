using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshRenderer _renderer;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int random =Random.Range(0, 8);
            Debug.Log(random);  
            DrawBloodParticle(random);
        }
    }

    private void DrawBloodParticle(int idx)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 2);
        vertices[2] = new Vector3(2, 2);
        vertices[3] = new Vector3(2, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        uv[0] = new Vector2(0.125f * idx, 0.5f);
        uv[1] = new Vector2(0.125f * idx, 1f);
        uv[2] = new Vector2(0.125f * (idx + 1), 1);
        uv[3] = new Vector2(0.125f * (idx+1), 0.5f);

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        _meshFilter.mesh = mesh;
    }
}
