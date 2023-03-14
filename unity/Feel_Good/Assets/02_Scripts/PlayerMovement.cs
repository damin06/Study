using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;
    private Animator animator;

    private Camera followCam;

    public float targetSpeed = 6f;

    public float currentSpeed => new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;



    // Start is called before the first frame update
    void Start()
    {
        //������Ʈ �������� 
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        followCam = Camera.main;
    }

    private void FixedUpdate()
    {
        Move(playerInput.moveInput);
        Rotate();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateAnimation(playerInput.moveInput);
    }

    public void Move(Vector2 moveInput)
    {
        var Direction = Vector3.Normalize(transform.forward * moveInput.y + transform.right * moveInput.x);
        var velocity = Direction * targetSpeed;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Rotate()
    {
        if (currentSpeed > 0)
        {
            var targetRotaion = followCam.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * targetRotaion;
        }
    }

    private void UpdateAnimation(Vector2 moveInput)
    {
        animator.SetFloat("Vertical Move", moveInput.y);
        animator.SetFloat("Horizontal Move", moveInput.x);
    }

}
