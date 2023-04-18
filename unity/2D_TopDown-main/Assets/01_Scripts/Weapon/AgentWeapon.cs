using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentWeapon : MonoBehaviour
{
    private float _desireAngle;
    protected WeaponRenderer _weaponRenderer;
    protected Weapon _weapon;

    public UnityEvent<int, int> OnChangeTotalAmmo;
    [SerializeField] private ReloadGaugeUI _reloadUI = null;
    [SerializeField] private AudioClip _cannotSound = null;
    [SerializeField] private int _maxTotalAmmo = 9999, _totalAmmo = 300;

    public int TotalAmmo
    {
        get => _totalAmmo;
        set
        {
            _totalAmmo = value;
            Mathf.Clamp(_totalAmmo, 0, _maxTotalAmmo);
            OnChangeTotalAmmo?.Invoke(_weapon.Ammo, _totalAmmo);
        }
    }

    private AudioSource _audioSource;
    private bool _isReloading = false;
    public bool IsReloading => _isReloading;

    protected virtual void Awake()
    {
        _weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        _weapon = GetComponentInChildren<Weapon>();

        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        OnChangeTotalAmmo?.Invoke(_weapon.Ammo, _totalAmmo);
    }

    #region ���ε� ���� ����
    public void Reload()
    {
        if (_isReloading == false && _totalAmmo > 0 && _weapon.AmmoFull == false)
        {
            _isReloading = true;
            _weapon.StopShooting();
            StartCoroutine(ReloadCoroutine());
        }
        else
        {
            PlayClip(_cannotSound);
        }
    }

    IEnumerator ReloadCoroutine()
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

        int reloadedAmmo = Mathf.Min(_totalAmmo, _weapon.EmptyBullet);
        _totalAmmo -= reloadedAmmo;
        _weapon.Ammo += reloadedAmmo;

        OnChangeTotalAmmo?.Invoke(_weapon.Ammo, _totalAmmo); //현재 총의 탄창수와 내가 가진 탄창 수

        _isReloading = false;
    }

    public void AddAmmo(int count)
    {
        TotalAmmo += count;
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
