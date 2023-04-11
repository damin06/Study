using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{


    [SerializeField]
    private LayerMask whatIsGround;

   // public event Action OnAttackKeyPress = null;
    //public event Action<Vector3> OnMoveKeyPress = null;

    private Camera mainCam;

    public Vector2 moveDir { get; private set; }
    public float mouseX { get; private set; }

    public bool isJump { get; private set; }
    public bool reload { get; private set; }
    public bool fire { get; private set; }

    public Vector3 mousePos { get; private set; }


    private void Awake()
    {
        mainCam = Camera.main;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveDir = new Vector2(x, y);

        mouseX = Input.GetAxis("Mouse X");

        reload = Input.GetButtonDown("Reload");
        fire = Input.GetButtonDown("Fire1");

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float depth = mainCam.farClipPlane;
        if(Physics.Raycast(ray, out hit, depth))
        {
            mousePos = hit.point;
        }
    }
    /*
    private void UpdateAttackInput()
    {
        if (Input.GetMouseButton(0)) OnAttackKeyPress?.Invoke();
     }
    private void UpdateAimInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 hitPoint;
            if (GetMouseWorldPosition(out hitPoint))
            {
                OnMoveKeyPress?.Invoke(hitPoint);
            }
        }
    }*/


    public bool GetMouseWorldPosition(out Vector3 hitPoint)
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        //스크린에 있는 마우스의 위치를 향하는 Ray를 만든다.
        RaycastHit hit;

        bool result = Physics.Raycast(ray, out hit, mainCam.farClipPlane);
        hitPoint = result ? hit.point : Vector3.zero;
        return result;
    }


}
