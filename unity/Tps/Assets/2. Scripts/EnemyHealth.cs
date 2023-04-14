using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    public override void OnDamage(float damage, Vector3 hitPostion, Vector3 hitNormal)
    {
        Debug.Log("786");
        base.OnDamage(damage, hitPostion, hitNormal);
        StartCoroutine(ShowBloodEffect(hitPostion, hitNormal));
    }

    private IEnumerator ShowBloodEffect(Vector3 hitPosition, Vector3 hitNormal)
    {
        EffectManager.Instance.PlayHitEffect(hitPosition, -hitNormal, transform, EffectManager.EffectType.Flesh);
        yield return new WaitForSeconds(1f);
    }

    public override void Die()
    {
        base.Die();
    }
}
