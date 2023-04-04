using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/BulletData")]
public class BulletDataSO : ScriptableObject
{
    public int damage = 1;
    public float bulletSpeed = 1;

    public GameObject impactObstaclePrefab;
    public GameObject impactEnemyPrefab;

    public float lifeTime = 1.5f;
}
