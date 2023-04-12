using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Weapon/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [Range(0, 999)] public int ammoCapacity = 100;
    public bool autoFire;

    [Range(0.1f, 2f)] public float weaponDelay = 0.1f;
    [Range(0, 10f)] public float spreadAngle = 5f;

    public int bulletCount = 1;

    public float reloadTime = 1f;
    public AudioClip reloadClip;
}
