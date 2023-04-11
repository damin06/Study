using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour, IDamageable
{
    public Texture[] textures;
    private MeshRenderer render;


    public GameObject expEffect; //����ȿ��
    private Transform tr;
    private Rigidbody rb;

    //�Ѿ� ���� Ƚ�� ����
    private int hitCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        render = GetComponentInChildren<MeshRenderer>();
        int idx = Random.Range(0, textures.Length);
        render.material.mainTexture = textures[idx];
    }

    public virtual void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        //�ǰݷ�, �ǰ� ��ġ, �ǰ� ����

        if (++hitCount == 3)
        {
            //����, ����
            ExpBarrel();
        }
        else
        {
            AttackBarrel(damage, hitPosition, hitNormal);
        }
    }

    void ExpBarrel()
    {
        //���� ȿ�� ��ƼŬ ����
        GameObject exp = Instantiate(expEffect, tr.position, tr.rotation);
        Destroy(exp, 2.0f);

        rb.mass = 1.0f; //���� �ڱ�ġ���� ���Ը� ������ ��
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        Destroy(gameObject, 2.0f);

    }

    void AttackBarrel(float power, Vector3 hitPosition, Vector3 hitDir)
    {
        EffectManager.Instance.PlayHitEffect(hitPosition, hitDir);
        rb.AddForce(hitDir * -1 * power, ForceMode.Impulse);
    }

}
