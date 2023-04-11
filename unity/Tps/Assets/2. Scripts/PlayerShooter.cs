using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    public Gun gun;
    private Animator animator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();

    }


    private void Update()
    {
        if (playerInput.fire)
        {
            gun.Fire();
        }
        else if (playerInput.reload)
        {
            if (gun.Reload()) animator.SetTrigger("Reload");
        }
    }


}
