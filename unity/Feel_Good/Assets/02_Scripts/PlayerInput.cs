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
    public bool fire => Input.GetKey(fireButtonName);
    public bool reload => Input.GetKeyDown(reloadButtonName);


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw(moveAxisName), Input.GetAxisRaw(rotateAxisName));
        if (moveInput.sqrMagnitude > 1)
        {
            moveInput = moveInput.normalized;
        }
    }


}
