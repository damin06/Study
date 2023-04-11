using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour, IDamageable
{
    public Texture[] textures;
    private MeshRenderer render;


    public GameObject expEffect; //폭발효과
    private Transform tr;
    private Rigidbody rb;

    //총알 맞은 횟수 누적
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
        //피격량, 피격 위치, 피격 방향

        if (++hitCount == 3)
        {
            //죽음, 폭발
            ExpBarrel();
        }
        else
        {
            AttackBarrel(damage, hitPosition, hitNormal);
        }
    }

    void ExpBarrel()
    {
        //폭발 효과 파티클 생성
        GameObject exp = Instantiate(expEffect, tr.position, tr.rotation);
        Destroy(exp, 2.0f);

        rb.mass = 1.0f; //위로 솟구치도록 무게를 가볍게 함
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        Destroy(gameObject, 2.0f);

    }

    void AttackBarrel(float power, Vector3 hitPosition, Vector3 hitDir)
    {
        EffectManager.Instance.PlayHitEffect(hitPosition, hitDir);
        rb.AddForce(hitDir * -1 * power, ForceMode.Impulse);
    }

}
