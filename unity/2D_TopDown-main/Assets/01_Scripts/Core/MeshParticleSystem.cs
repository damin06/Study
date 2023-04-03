using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeshParticleSystem : MonoBehaviour
{
    private const int MAX_QUAD_AMOUNT = 15000;
    [Serializable]
    public struct ParticleUVPixel
    {
        public Vector2Int uvOOPixel;
        public Vector2Int uv11Pixel;
    }

    public struct UVCoords
    {
        public Vector2 uvOO;
        public Vector2 uv11;
    }

    [SerializeField]
    private ParticleUVPixel[] _uvPixelArr;
    private UVCoords[] _uvCoordArr;

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private Vector3[] _verticles;
    private Vector2[] _uv;
    private int[] _triangles;


    private void Awake()
    {
        _mesh = new Mesh();
        _verticles = new Vector3[4 * MAX_QUAD_AMOUNT];
        _uv = new Vector2[6 * MAX_QUAD_AMOUNT];
        _triangles = new int[6 * MAX_QUAD_AMOUNT];

        _mesh.vertices = _verticles;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;

        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
        _meshRenderer = GetComponent<MeshRenderer>();

        _meshRenderer.sortingLayerName = "Agent";
        _meshRenderer.sortingOrder = 0;

        Texture mainTex = _meshRenderer.material.mainTexture;
        int twidth = mainTex.width;
        int tHeight = mainTex.height;

        List<UVCoords> uVCoords = new List<UVCoords>();

        var uVCoordList = new List<UVCoords>();
        foreach (ParticleUVPixel pixelUV in _uvPixelArr)
        {
            UVCoords temp = new UVCoords
            {
                uvOO = new Vector2((float)pixelUV.uvOOPixel.x / twidth, (float)pixelUV.uvOOPixel.y / tHeight),
                uv11 = new Vector2((float)pixelUV.uv11Pixel.x / twidth, (float)pixelUV.uv11Pixel.y / tHeight),
            };
            uVCoords.Add(temp);
        }
        _uvCoordArr = uVCoordList.ToArray();
    }

    public int GetRandomBloodIndex()
    {
        return UnityEngine.Random.Range(0, 8);
    }

    public int GetRandomShellIndex()
    {
        return UnityEngine.Random.Range(8, 9);
    }

    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            AddQuad(pos, 0, new Vector3(1, 1, 0), false, GetRandomShellIndex());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            AddQuad(pos, 0, new Vector3(1, 1, 0), false, GetRandomBloodIndex());
        }

    }

    private int _quadIndex = 0;

    private int AddQuad(Vector3 pos, float rot, Vector3 quadSize, bool skewed, int uvIndex)
    {
        UpdateQuad(_quadIndex, pos, rot, quadSize, skewed, uvIndex);

        int spawnedQuadIndex = _quadIndex;
        _quadIndex = (_quadIndex + 1) % MAX_QUAD_AMOUNT;

        return spawnedQuadIndex;
    }

    public void UpdateQuad(int quadIndex, Vector3 pos, float rot, Vector3 quadSize, bool skewed, int uvIndex)
    {
        int vIndex0 = quadIndex * 4;
        int vIndex1 = vIndex0 + 1;
        int vIndex2 = vIndex0 + 2;
        int vIndex3 = vIndex0 + 3;

        if (skewed)
        {

        }
        else
        {
            _verticles[vIndex0] = pos + Quaternion.Euler(0, 0, rot - 180) * quadSize;
            _verticles[vIndex1] = pos + Quaternion.Euler(0, 0, rot - 270) * quadSize;
            _verticles[vIndex2] = pos + Quaternion.Euler(0, 0, rot - 0) * quadSize;
            _verticles[vIndex3] = pos + Quaternion.Euler(0, 0, rot - 90) * quadSize;
        }

        UVCoords uv = _uvCoordArr[uvIndex];
        _uv[vIndex0] = uv.uvOO;
        _uv[vIndex1] = new Vector2(uv.uvOO.x, uv.uv11.y);
        _uv[vIndex2] = uv.uv11;
        _uv[vIndex3] = new Vector2(uv.uv11.x, uv.uv11.y);

        int tIndex = quadIndex * 6;
        _triangles[tIndex + 0] = vIndex0;
        _triangles[tIndex + 1] = vIndex1;
        _triangles[tIndex + 2] = vIndex2;

        _triangles[tIndex + 3] = vIndex0;
        _triangles[tIndex + 4] = vIndex2;
        _triangles[tIndex + 5] = vIndex3;

        _mesh.vertices = _verticles;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;


    }
}