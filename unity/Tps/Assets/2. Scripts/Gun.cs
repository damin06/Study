using System;
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
    public Transform firePosition;
    public ParticleSystem muzzleFlashEffect; //�ѱ� ȭ��
    public float bulletLineEffetTime = 0.03f; //���η����� ���� �ð�

    private LineRenderer bulletLineRenderer;
    public float damage = 25;
    private float fireDistance = 50f;
    public int magCapacity = 10; //źâ�뷮
    public int magAmmo;  //���� ���� ź��
    public float timeBetFire = 0.12f; //ź�� �߻� ����
    public float reloadTime = 1.0f; //������ �ҿ�ð�
    public float lastFireTime; //���� ���������� �߻��� �ð�


    //����� �ҽ�
    private AudioSource audioSource;
    public AudioClip shootAudio;
    public AudioClip reloadAudio;

    private void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        magAmmo = magCapacity;
        state = State.Ready;
        lastFireTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Fire()
    {
        //�߻� ������ ����
        if (state == State.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
            return true;
        }
        return false;
    }

    private void Shot()
    {
        //����ĳ��Ʈ
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        //��� ��ġ, ��� ����, out hit
        if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, fireDistance))
        {
            var target = hit.collider.GetComponent<IDamageable>();
            //�Ѿ˿� �¾Ұ� �ش� �浹ü�� �������� ���� �� �ִٸ� OnDamage�� ���� ������ ó��
            if (target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
            }
            else
            {
                //�Ѿ˿� �¾����� �ش� �浹ü�� �������� ���� �ʴ� ��ü��� ��Ż ȿ���ֱ�
                EffectManager.Instance.PlayHitEffect(hit.point, hit.normal, hit.transform);
            }
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = firePosition.position + firePosition.forward * fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));
        magAmmo--;
        if (magAmmo <= 0)
        {
            state = State.Empty;
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        audioSource.clip = shootAudio;
        audioSource.Play();
        muzzleFlashEffect.Play();
        bulletLineRenderer.SetPosition(0, firePosition.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(bulletLineEffetTime);
        bulletLineRenderer.enabled = false;
    }

    public bool Reload()
    {
        //�������� �������� ���� ����(����, ź��)
        if (state == State.Reloading || magAmmo >= magCapacity)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        return true;
    }


    public IEnumerator ReloadRoutine()
    {
        audioSource.clip = reloadAudio;
        audioSource.Play();

        state = State.Reloading;

        yield return new WaitForSeconds(reloadTime);
        magAmmo = magCapacity;
        state = State.Ready;
    }



}
