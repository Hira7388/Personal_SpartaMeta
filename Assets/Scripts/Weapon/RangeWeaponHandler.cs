using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Range Weapon Info")]
    [SerializeField] private Transform projectileSpawnPosition; // 발사체 소환 위치
    [SerializeField] private int projectileIndex;
    public int ProjectileIndex { get => projectileIndex; }

    [SerializeField] private float multipleProjectileSpread; // 발사체 퍼짐 정도
    public float MultipleProjectileSpread { get => multipleProjectileSpread; }

    [SerializeField] private int numberProjectilePerShot; // 동시에 발사하는 발사체 개수
    public int NumberProjectilePerShot { get => numberProjectilePerShot; }

    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get => projectileColor; }


    // 컴포넌트
}
