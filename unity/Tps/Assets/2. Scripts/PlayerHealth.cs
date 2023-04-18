using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public override void OnDamage(float damage, Vector3 hitPostion, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPostion, hitNormal);
        CameraAction.Instance.ShakeCam(4f, 0.2f);

        StartCoroutine(ShowBloodEffet(hitPostion, hitNormal));
    }

    private IEnumerator ShowBloodEffet(Vector3 hitPosition, Vector3 hit)
    {
        yield return new WaitForSeconds(1f);

    }
}
