using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentWeapon : MonoBehaviour
{
    private float _desireAngle; //무기가 바라봐야 하는 방향

    protected WeaponRenderer _weaponRenderer;
    protected Weapon _weapon;

    public UnityEvent<int, int> OnChangeTotalAmmo;
    [SerializeField]
    private ReloadGaugeUI _reloadUI = null;
    [SerializeField]
    private AudioClip _cannotSound = null;

    [SerializeField]
    private int _maxTotalAmmo = 9999, _totalAmmo = 300; //최대 9999발, 최초 300발 가지고 시작

    private AudioSource _audioSource;
    private bool _isReloading = false;
    public bool IsReloading => _isReloading;

    protected virtual void Awake()
    {
        _weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        _weapon = GetComponentInChildren<Weapon>(); //자기가 들고 있는 총 가져오기

        _audioSource = GetComponent<AudioSource>();
    }

    #region 리로딩 관련 로직
    public void Reload()
    {
        if(_isReloading == false && _totalAmmo > 0 && _weapon.AmmoFull == false)
        {
            _isReloading = true;
            _weapon.StopShooting(); //사격중지
            StartCoroutine(ReloadCoroutine());
        }else
        {
            PlayClip(_cannotSound);
        }   
    }

    IEnumerator ReloadCoroutine()
    {
        _reloadUI.gameObject.SetActive(true); //리로드 UI 나오게 해주고
        float time = 0;
        while(time <= _weapon.WeaponData.reloadTime)
        {
            _reloadUI.ReloadGaugeNormal(time / _weapon.WeaponData.reloadTime);
            time += Time.deltaTime;
            yield return null;
        }

        _reloadUI.gameObject.SetActive(false); //리로드 게이지 꺼주고
        if(_weapon.WeaponData.reloadClip != null)
            PlayClip(_weapon.WeaponData.reloadClip);

        int reloadedAmmo = Mathf.Min(_totalAmmo, _weapon.EmptyBulletCnt);
        _totalAmmo -= reloadedAmmo;
        _weapon.Ammo += reloadedAmmo;

        _isReloading = false;

    }

    private void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
    #endregion



    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position; //마우스 방향 벡터를 구하고
        
        _desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //디그리 각도를 구한다.

        AdjustWeaponRendering();

        transform.rotation = Quaternion.AngleAxis(_desireAngle, Vector3.forward);
        //z축 기준으로 회전
    }

    private void AdjustWeaponRendering()
    {
        if(_weaponRenderer != null)
        {
            _weaponRenderer.FlipSprite(_desireAngle > 90f || _desireAngle < -90f);
            _weaponRenderer.RenderBehindHead(  _desireAngle > 0 && _desireAngle < 180 );
        }
    }

    public virtual void Shoot()
    {
        //자기가 무기를 들고 있을 경우 발사
        if (_isReloading) return; //리로딩중에는 발사 금지
        _weapon?.TryShooting();
    }

    public virtual void StopShooting()
    {
        _weapon?.StopShooting();
    }
}
