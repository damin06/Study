using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, Iitem
{
    public float newHealth = 20;
    public void use(GameObject target)
    {
        LivingEntity health = target.GetComponent<LivingEntity>();

        if (health == null)
            return;

        health.RestoreHealth(newHealth);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
