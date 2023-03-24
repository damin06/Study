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
    public float rotationSpeed = 4f;

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

    public void Rotate() //마우스 방향으로 보기
    {
        var targetRotaion = followCam.transform.eulerAngles.y;
        transform.eulerAngles = Vector3.up * targetRotaion;
    }

    public void SetRotation()
    {
        Vector3 target;
        bool isHit = playerInput.GetMousePos(out target);

        if (isHit)
        {
            Vector3 dir = target - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }


        target.y = 0;
        Vector3 v = target - transform.position;

        float degree = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;

        float rot = Mathf.LerpAngle(transform.eulerAngles.y, degree, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, rot, 0);

        // Vector3 target = playerInput.moveInput;
        // target.y = 0;
        // Vector3 v = target - transform.position;

        // float dgree = Mathf.Atan2(v.x, v.z);
    }

    private void UpdateAnimation(Vector2 moveInput)
    {
        animator.SetFloat("Vertical Move", moveInput.y);
        animator.SetFloat("Horizontal Move", moveInput.x);
    }

}
