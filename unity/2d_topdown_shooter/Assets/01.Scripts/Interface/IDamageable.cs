using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public bool IsEnemy { get; }
    public Vector3 _hitPoint { get; }
    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal);
}
