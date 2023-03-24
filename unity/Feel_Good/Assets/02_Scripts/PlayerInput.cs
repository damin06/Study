using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Horizontal";
    public string rotateAxisName = "Vertical";
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload";


    public Vector2 moveInput { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }

    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    public Action OnFirePressed = null;

    void Update()
    {
        moveInput = new Vector2(Input.GetAxis(moveAxisName),
            Input.GetAxis(rotateAxisName));
        if (moveInput.sqrMagnitude > 1) moveInput = moveInput.normalized;

        //fire 키 받기
        fire = Input.GetButtonDown(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);

        if (fire)
        {
            OnFirePressed?.Invoke();
        }
        //reload 키 받기
    }

    public bool GetMousePos(out Vector3 point)
    {
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        float depth = mainCam.farClipPlane;

        point = Vector3.zero;
        if (Physics.Raycast(cameraRay, out hit, depth))
        {
            point = hit.point;
            return true;
        }
        else
        {
            return false;
        }
    }

}
