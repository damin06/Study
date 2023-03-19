using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }
    public State state { get; private set; }

    public Transform firePosition; //총알나가는 위치와 방향
    public ParticleSystem muzzleFlashEffect;
    public float bulletLineEffectTime = 0.03f;

    private LineRenderer bulletLineRenderer;
    public float damage = 25;

    public float fireDistance = 100f; //발사가능 거리

    public int magCapacity = 30; //탄창 용량
    public int magAmmo; //현재 탄창에 있는 탄약수
    public float timeBetFire = 0.12f; //탄알 발사 간격
    public float reloadTime = 1.8f; //재장전 소요시간
    private float lastFireTime; //총을 마지막으로 발사한 시간

    private void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void Start()
    {
        magAmmo = magCapacity;
        state = State.Ready;
        lastFireTime = 0;
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();
        bulletLineRenderer.enabled = true;
        bulletLineRenderer.SetPosition(0, firePosition.position);
        bulletLineRenderer.SetPosition(1, hitPosition);

        yield return new WaitForSeconds(bulletLineEffectTime);

        bulletLineRenderer.enabled = false;
    }

    public IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        yield return new WaitForSeconds(reloadTime);
        magAmmo = magCapacity;
        state = State.Ready;
    }

    public bool Fire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
        }
        return false;
    }


    private void Shot()
    {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, fireDistance))
        {
            var target = hit.collider.GetComponent<IDamage>();
            if (target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;
            StartCoroutine(ShotEffect(hitPosition));
        }
        else
        {
            hitPosition = firePosition.position + firePosition.forward * fireDistance;
        }

        magAmmo--;
        if (magAmmo == 0)
        {
            state = State.Empty;
        }
    }

    public bool Reload()
    {
        if (state == State.Reloading || magCapacity == magAmmo)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        return true;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }

        if (Input.GetButton("Reload"))
        {
            StartCoroutine(ReloadRoutine());
        }
    }
}
