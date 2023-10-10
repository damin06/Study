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
        /* isOwner을 체크해서 
         * 마우스 포지션을 가져온다. _inputReader.AimPosition
         * 마우스 포지션을 이용해서 _turretTrm 을 알잘딱 돌려줍니다. 
         * 끝
         */
        if (!IsOwner) return; //이거때문에 NetworkBehaviour를 쓴다.
        Vector2 mousePos = _inputReader.AimPosition;

        Vector3 worldMousePos = CameraManager.Instance.MainCam.ScreenToWorldPoint(mousePos);
        Vector3 dir = (worldMousePos - transform.position).normalized;

        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        //_turretTrm.rotation = Quaternion.Euler(0, 0, angle);

        _turretTrm.up = new Vector2(dir.x, dir.y);

    }
}
