using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    private float _desireAngle;
    protected WeaponRenderer _weaponRenderer;
    protected Weapon _waepon;

    protected virtual void Awake()
    {
        _weaponRenderer = GetComponent<WeaponRenderer>();
        _waepon = GetComponentInChildren<Weapon>();
    }

    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position;
        //마우스 방향 벡터를 구함

        _desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //디그리 각도를 구한다.

        AdjustWeaponRendering();

        transform.rotation = Quaternion.AngleAxis(_desireAngle, Vector3.forward);
        //z 기준으로 회전
    }

    private void AdjustWeaponRendering()
    {
        if (_weaponRenderer != null)
        {
            _weaponRenderer.FlipSprite(_desireAngle > 90f || _desireAngle < -90f);
            _weaponRenderer.RenderBehindHead(_desireAngle > 0 && _desireAngle < 180);
        }
    }

    public virtual void Shoot()
    {
        _waepon?.TryShooting();
    }

    public virtual void StopShooting()
    {
        _waepon?.StopShooting();

    }
}
