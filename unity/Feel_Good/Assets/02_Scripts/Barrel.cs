using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IDamage
{
    [SerializeField] private Texture[] textures;
    private MeshRenderer render;


    public GameObject expEffect;
    private Transform tr;
    private Rigidbody rb;
    private int hitCount;



    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();


        render = GetComponent<MeshRenderer>();
        int idx = Random.Range(0, textures.Length);
        render.material.mainTexture = textures[idx];
    }

    public void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        if (++hitCount == 3)
        {
            ExpBarrel();
        }
        else
        {
            AttackBarrel(damage, -hitNormal);
        }
    }

    private void AttackBarrel(float power, Vector3 dir)
    {
        rb.AddForce(dir * power, ForceMode.Impulse);
    }

    private void ExpBarrel()
    {
        var exp = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(exp, 2);

        rb.mass = 1;
        rb.AddForce(Vector3.up * 100, ForceMode.Impulse);
    }
}
