using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAiming : NetworkBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _turretTrm;

    private void LateUpdate()
    {
        /*isOwner을 체크한다
         * 마우스 포지션을 가져온다. _inputReader.AimPosition
         * 마우스 포지션을 이용해서 _turretTrm 을 알잘딱 돌려줍니다.
         * 끝
         */

        if (!IsOwner) return;

        Vector3 mousePos = CameraManager.Instance.MinCam.ScreenToWorldPoint(_inputReader.AimPosition);
        Vector3 dir = (mousePos - transform.position).normalized;
        _turretTrm.up = new Vector2(dir.x, dir.y);
    }
}
