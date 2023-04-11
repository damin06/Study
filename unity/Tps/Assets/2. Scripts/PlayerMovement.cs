using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f, gravity = -9.8f;

    [HideInInspector] public Vector3 dir;
    CharacterController characterController;
    PlayerInput playerInput;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        PlayerMove();
        //PlayerRotate();
        UpdateAnimation(playerInput.moveDir);
    }

    void PlayerMove()
    {
        dir = transform.forward * playerInput.moveDir.y + transform.right * playerInput.moveDir.x;
        characterController.Move(dir * moveSpeed * Time.deltaTime);
    }

 
    void PlayerRotate()
    {
        Vector3 target = playerInput.mousePos;
        Vector3 dir = target - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir.normalized);
        
    }

    private void UpdateAnimation(Vector2 moveInput)
    {
        animator.SetFloat("Vertical Move", moveInput.y);
        animator.SetFloat("Horizontal Move", moveInput.x);
    }

}
