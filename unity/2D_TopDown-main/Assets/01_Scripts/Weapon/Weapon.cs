using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO _weaponDataSO;
    [SerializeField] protected Transform _muzzle;
    [SerializeField] protected Transform _shellEjectPosition;

    public WeaponDataSO WeaponData => _weaponDataSO;

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting;
    protected bool _isShooting;
    protected bool _delayCoroutine = false;

    #region AMMO 관련 코드들
    protected int _ammo;
    public int Ammo
    {
        get { return _ammo; }
        set 
        { 
            _ammo = Mathf.Clamp(value, 0, _weaponDataSO.ammoCapacity);
        }
    }
    public bool AmmoFull => Ammo == _weaponDataSO.ammoCapacity;
    public int EmptyBullet => _weaponDataSO.ammoCapacity - _ammo;
    #endregion

    private void Awake()
    {
        _ammo = _weaponDataSO.ammoCapacity;
    }

    private void Update()
    {
        UseWeapon();
    }

    public void UseWeapon()
    {
        if(_isShooting && _delayCoroutine == false)
        {
            if (Ammo > 0)
            {
                OnShoot?.Invoke();
                for (int i = 0; i < _weaponDataSO.bulletCount; i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                _isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishOneShooting();
        }
    }

    private void FinishOneShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if(_weaponDataSO.autoFire == false)
        {
            _isShooting = true;
        }
    }

    private IEnumerator DelayNextShootCoroutine()
    {
        _delayCoroutine = true;
        yield return new WaitForSeconds(_weaponDataSO.weaponDelay);
        _delayCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(_muzzle.position, CalculateAngle(_muzzle));
    }


    private Quaternion CalculateAngle(Transform muzzle)
    {
        float spread = Random.Range(-_weaponDataSO.spreadAngle, _weaponDataSO.spreadAngle);
        Quaternion bulletSpreadRot = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRot;
    }
    private void SpawnBullet(Vector3 position, Quaternion rot)
    {
        RegularBullet b = PoolManager.Instance.Pop("Bullet") as RegularBullet;
        b.SetPositionAndRotation(position, rot);
        b.IsEnemy = false;
    }

    public void TryShooting()
    {
        _isShooting = true;
    }

    public void StopShooting()
    {
        _isShooting = false;
        OnStopShooting?.Invoke();
    }
}
