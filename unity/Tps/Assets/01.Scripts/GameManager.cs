using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class GameManager : MonoBehaviour
{

    private NavMeshSurface _navSurface;
    public static GameManager Instance;

    [SerializeField]
    private LayerMask _whatIsBase;

    private Camera _mainCam;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;

        _navSurface = GetComponent<NavMeshSurface>();
    }
    private void Start()
    {
        _mainCam = Camera.main; //���� ī�޶� ĳ��
    }

    private void ReBakeMesh()
    {
        _navSurface.BuildNavMesh();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool result = Physics.Raycast(ray, out hit, _mainCam.farClipPlane, _whatIsBase);
            if (result)
            {
                BaseBlock block = hit.collider.GetComponent<BaseBlock>();

                block?.ClickBaseBlock();

                ReBakeMesh();
            }
        }
    }

    public bool GetMouseWorldPosition(out Vector3 pos)
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool result = Physics.Raycast(ray, out hit, _mainCam.farClipPlane, _whatIsBase);
        if (result)
        {
            pos = hit.point;
            return true;
        }
        pos = Vector3.zero;
        return false;
    }
}
