using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [Range(0, 999)] public int ammoCapacity = 100; //탄창 크기
    public bool autoFire; //누르고 있는 동안 연사? 또는 단발

    [Range(0.1f, 2f)] public float weaponDelay = 0.1f; //사격 지연 시간
    [Range(0, 10f)] public float spreadAngle = 5f; //탄이 퍼지는 각도

    public int bulletCount = 1;

    public float reloadTime = 1f;
    public AudioClip reloadClip;
}
