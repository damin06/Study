using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    private float _desireAngle;
    protected WeaponRenderer _weaponRenderer;
    protected Weapon _weapon;

    protected virtual void Awake()
    {
        _weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position; //���콺 ���� ���� ���ϱ�

        _desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; //��׸� ������ ���Ѵ�

        AdjustWeaponRendering();

        transform.rotation = Quaternion.AngleAxis(_desireAngle, Vector3.forward); //z�� �������� ȸ��
    }


    private void AdjustWeaponRendering()
    {
        if(_weaponRenderer != null)
        {
            _weaponRenderer.FlipSprite(_desireAngle > 90f || _desireAngle < -90f);
            _weaponRenderer.RendererBehindeHead(_desireAngle > 0 && _desireAngle < 180) ;
        }
    }

    public virtual void Shoot()
    {
        _weapon?.TryShooting();
    }

    public virtual void StopShooting()
    {
        _weapon?.StopShooting();
    }
}
