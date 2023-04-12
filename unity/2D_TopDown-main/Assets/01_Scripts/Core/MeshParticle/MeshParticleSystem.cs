using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class MeshParticleSystem : MonoBehaviour
{
    public const int MAX_QUAD_AMOUNT = 15000;

    [Serializable]
    public struct ParticleUVPixel
    {
        public Vector2Int uv00Pixel;
        public Vector2Int uv11Pixel;
    }

    public struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }

    [SerializeField]
    private ParticleUVPixel[] uvPixelArr;
    private UVCoords[] _uvCoordArr;

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private Vector3[] _vertices;
    private Vector2[] _uv;
    private int[] _triangles;

    private bool _updateVertices;
    private bool _updateUV;
    private bool _updateTriangles;

    private void Awake()
    {
        _mesh = new Mesh();
        _vertices = new Vector3[4 * MAX_QUAD_AMOUNT];
        _uv = new Vector2[4 * MAX_QUAD_AMOUNT];
        _triangles = new int[6 * MAX_QUAD_AMOUNT];

        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;

        _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);

        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
        _meshRenderer = GetComponent<MeshRenderer>();

        _meshRenderer.sortingLayerName = "Agent";
        _meshRenderer.sortingOrder = 0;

        Texture mainTex = _meshRenderer.material.mainTexture; //Diffuse에 넣어준 스프라이트를 가져온다
        int tWidth = mainTex.width;
        int tHeight = mainTex.height;

        List<UVCoords> uvCoordList = new List<UVCoords>();
        foreach (ParticleUVPixel pixelUV in uvPixelArr)
        {
            UVCoords temp = new UVCoords
            {
                uv00 = new Vector2((float)pixelUV.uv00Pixel.x / tWidth, (float)pixelUV.uv00Pixel.y / tHeight),
                uv11 = new Vector2((float)pixelUV.uv11Pixel.x / tWidth, (float)pixelUV.uv11Pixel.y / tHeight),
            };
            uvCoordList.Add(temp);
        }

        _uvCoordArr = uvCoordList.ToArray();
    }

    public int GetRandomBloodIndex()
    {
        return Random.Range(0, 8);
    }

    public int GetRandomShellIndex()
    {
        return Random.Range(8, 9);
    }
    private int cnt = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            Vector3 quadSize = new Vector3(1, 1, 0);
            float rot = 0;
            int uvIndex = GetRandomShellIndex();
            int qIndex = AddQuad(pos, rot, new Vector3(1, 1, 0), false, uvIndex);
            FunctionUpdater.Instance.Create(() =>
            {
                pos += new Vector3(1, 1) * 0.8f * Time.deltaTime;
                quadSize += new Vector3(1, 1) * Time.deltaTime;
                rot += 360f * Time.deltaTime;

                UpdateQuad(qIndex, pos, rot, quadSize, false, uvIndex);
            });
        }
    }

    private int _quadIndex = 0;

    public int AddQuad(Vector3 pos, float rot, Vector3 quadSize, bool skewed, int uvIndex)
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
            _vertices[vIndex0] = pos + Quaternion.Euler(0, 0, rot - 180) * quadSize;
            _vertices[vIndex1] = pos + Quaternion.Euler(0, 0, rot - 270) * quadSize;
            _vertices[vIndex2] = pos + Quaternion.Euler(0, 0, rot - 0) * quadSize;
            _vertices[vIndex3] = pos + Quaternion.Euler(0, 0, rot - 90) * quadSize;
        }

        UVCoords uv = _uvCoordArr[uvIndex];
        _uv[vIndex0] = uv.uv00;
        _uv[vIndex1] = new Vector2(uv.uv00.x, uv.uv11.y);
        _uv[vIndex2] = uv.uv11;
        _uv[vIndex3] = new Vector2(uv.uv11.x, uv.uv00.y);

        int tIndex = quadIndex * 6;

        _triangles[tIndex + 0] = vIndex0;
        _triangles[tIndex + 1] = vIndex1;
        _triangles[tIndex + 2] = vIndex2;

        _triangles[tIndex + 3] = vIndex0;
        _triangles[tIndex + 4] = vIndex2;
        _triangles[tIndex + 5] = vIndex3;

        _updateVertices = true;
        _updateUV = true;
        _updateTriangles = true;
    }

    private void LateUpdate()
    {
        if(_updateVertices)
        {
            _mesh.vertices = _vertices;
            _updateVertices = false;
        }

        if(_updateUV)
        {
            _mesh.uv = _uv;
            _updateUV= false;
        }

        if(_updateTriangles)
        {
            _mesh.triangles = _triangles;
            _updateTriangles= false;
        }
    }

    public void DestroyQuad(int quadIndex)
    {
        int vIndex0 = quadIndex * 4;
        int vIndex1 = quadIndex + 1;
        int vIndex2 = quadIndex + 2;
        int vIndex3 = quadIndex + 3;

        _vertices[vIndex0] = Vector3.zero;
        _vertices[vIndex1] = Vector3.zero;
        _vertices[vIndex2] = Vector3.zero;
        _vertices[vIndex3] = Vector3.zero;

        _updateVertices = true;
    }

    public void DestroyAllQuad()
    {
        Array.Clear(_vertices, 0, _vertices.Length);
        _quadIndex = 0;
        _updateVertices = true;
    }
}