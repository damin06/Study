using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUV : MonoBehaviour
{
    [SerializeField] private Material met;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 10);
        vertices[2] = new Vector3(10, 10);

        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(0, 2);
        uv[2] = new Vector2(0, 3);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;

        meshRenderer.material = met;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
