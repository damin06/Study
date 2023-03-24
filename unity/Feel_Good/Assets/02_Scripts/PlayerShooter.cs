using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        playerInput.OnFirePressed += fireButtonHandle;
    }

    private void fireButtonHandle()
    {
        gun.Fire();
        playerMovement.SetRotation();
    }
    // Update is called once per frame
    void Update()
    {

        if (playerInput.reload)
        {
            //gun.Reload()
            //재장전 애니메이션 실행
        }
    }

}
