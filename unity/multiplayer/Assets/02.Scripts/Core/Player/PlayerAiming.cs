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
        /*isOwner�� üũ�Ѵ�
         * ���콺 �������� �����´�. _inputReader.AimPosition
         * ���콺 �������� �̿��ؼ� _turretTrm �� ���ߵ� �����ݴϴ�.
         * ��
         */

        if (!IsOwner) return;

        Vector3 mousePos = CameraManager.Instance.MinCam.ScreenToWorldPoint(_inputReader.AimPosition);
        Vector3 dir = (mousePos - transform.position).normalized;
        _turretTrm.up = new Vector2(dir.x, dir.y);
    }
}
