using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInput.fire)
        {
            gun.Fire();
        }
        else if (playerInput.reload)
        {
            //gun.Reload()
            //재장전 애니메이션 실행
        }
    }

}
