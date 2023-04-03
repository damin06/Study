using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class AgentWeapon : MonoBehaviour
{
    private float _desireAngle;
    protected WeaponRenderer _weaponRenderer;
    protected Weapon _weapon;

    public UnityEvent<int, int> OnChangeTototalAmmo;
    [SerializeField]
    private ReloadGaugeUI _reloadUI = null;
    [SerializeField]
    private AudioClip _cannotSound = null;

    [SerializeField]
    private int _maxTotalAmmo = 9999, _totalAmmo = 360;

    private AudioSource _audioSoruce;
    private bool _isReloading = false;
    private bool IsReloading => _isReloading;

    protected virtual void Awake()
    {
        _weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        _weapon = GetComponentInChildren<Weapon>();

        _audioSoruce = GetComponent<AudioSource>();
    }

    #region 리로딩 관련 로직
    public void Reload()
    {
        if (_isReloading == false && _totalAmmo > 0 && _weapon.AmmoFull == false)
        {
            _isReloading = true;
            _weapon.StopShooting();
            StartCoroutine(ReloadCorutine());
        }
        else
        {
            PlayClip(_cannotSound);
        }
    }

    IEnumerator ReloadCorutine()
    {
        _reloadUI.gameObject.SetActive(true);
        float time = 0;
        while (time <= _weapon.WeaponData.reloadTime)
        {
            _reloadUI.ReloadGaugeNormal(time / _weapon.WeaponData.reloadTime);
            time += Time.deltaTime;
            yield return null;
        }
        _reloadUI.gameObject.SetActive(false);
        if (_weapon.WeaponData.reloadClip != null)
            PlayClip(_weapon.WeaponData.reloadClip);

        int reloadAmmo = Mathf.Min(_totalAmmo, _weapon.EmptyBullet);
        _totalAmmo -= reloadAmmo;
        _weapon.Ammo += reloadAmmo;

        _isReloading = false;
    }

    private void PlayClip(AudioClip clip)
    {
        _audioSoruce.Stop();
        _audioSoruce.clip = clip;
        _audioSoruce.Play();
    }
    #endregion

    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position; //���콺 ���� ���� ���ϱ�

        _desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; //��׸� ������ ���Ѵ�

        AdjustWeaponRendering();

        transform.rotation = Quaternion.AngleAxis(_desireAngle, Vector3.forward); //z�� �������� ȸ��
    }


    private void AdjustWeaponRendering()
    {
        if (_weaponRenderer != null)
        {
            _weaponRenderer.FlipSprite(_desireAngle > 90f || _desireAngle < -90f);
            _weaponRenderer.RendererBehindeHead(_desireAngle > 0 && _desireAngle < 180);
        }
    }

    public virtual void Shoot()
    {
        if (_isReloading) return;
        _weapon?.TryShooting();
    }

    public virtual void StopShooting()
    {
        _weapon?.StopShooting();
    }
}
