using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //피격량, 피격위치, 피격당한 법선벡터
    void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal);
}
