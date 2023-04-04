using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO _weaponData;
    [SerializeField] protected Transform _muzzle; //총구 위치
    [SerializeField] protected Transform _shellEjectPosition; //탄피 배출 위치

    public WeaponDataSO WeaponData => _weaponData; //나중에 가져다 쓸 수 있게 겟터 만들어둔다.

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting; 
    protected bool _isShooting; //현재 발사중인가?
    protected bool _delayCoroutine = false;

    #region AMMO 관련 코드들
    protected int _ammo; //현재 총알수
    public int Ammo
    {
        get { return _ammo; }
        set
        {
            _ammo = Math.Clamp(value, 0, _weaponData.ammoCapacity);
        }
    }
    public bool AmmoFull => Ammo == _weaponData.ammoCapacity;
    public int EmptyBulletCnt => _weaponData.ammoCapacity - _ammo; //현재 부족한 탄환수
    #endregion

    private void Awake()
    {
        _ammo = _weaponData.ammoCapacity; //최대치로 처음 셋팅
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        //여기서 만약 총알을 발사하라고 명령이 왔고 딜레이가 없다면 발사를 할꺼야.
        if(_isShooting && _delayCoroutine == false )
        {
            //현재 총알의 잔량이 있는지도 체크를 해야하지만 
            if(Ammo >= _weaponData.bulletCount)
            {
                OnShoot?.Invoke();
                for (int i = 0; i < _weaponData.bulletCount; i++)
                {
                    ShootBullet();
                    Ammo--;
                }
            }else
            {
                _isShooting = false;
                OnShootNoAmmo?.Invoke(); //총알이 없다
                return;
            }
            FinishOneShooting(); //한발 쏘고 난다음에는 딜레이 코루틴을 돌려줘야 하니까 작업을 여기서
        }
    }

    private void FinishOneShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if(_weaponData.autoFire == false)
        {
            _isShooting = false;
        }
    }

    private IEnumerator DelayNextShootCoroutine()
    {
        _delayCoroutine = true;
        yield return new WaitForSeconds(_weaponData.weaponDelay);
        _delayCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(_muzzle.position, CalculateAngle(_muzzle));
    }

    private Quaternion CalculateAngle(Transform muzzle)
    {
        float spread = Random.Range(-_weaponData.spreadAngle, _weaponData.spreadAngle);
        Quaternion bulletSpreadRot = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRot; //
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
