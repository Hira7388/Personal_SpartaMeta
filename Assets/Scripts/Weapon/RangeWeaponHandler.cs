using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Range Weapon Info")]
    [SerializeField] private Transform projectileSpawnPosition; // 발사체 소환 위치
    [SerializeField] private int projectileIndex;
    public int ProjectileIndex { get => projectileIndex; }

    [SerializeField] private float spread; // 무기의 명중률
    public float Spread { get => spread; }

    [SerializeField] private float multipleProjectileSpread; // 멀티샷 발사체 탄 퍼짐 정도
    public float MultipleProjectileSpread { get => multipleProjectileSpread; }

    [SerializeField] private int numberProjectilePerShot; // 멀티샷 발사체 개수
    public int NumberProjectilePerShot { get => numberProjectilePerShot; }

    // 발사채 매니저 호출
    ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    // 공격 입력 시 처리하는 메서드
    public override void Attack()
    {
        base.Attack();

        float projectilesSpread = multipleProjectileSpread;
        int projectilesPerShot = numberProjectilePerShot;

        float minSpreadAngle = ( projectilesPerShot / 2f ) * projectilesSpread; // 멀티샷의 탄 퍼짐의 절반만큼

        for( int i = 0; i < projectilesPerShot; i++ )
        {
            float angle = minSpreadAngle + projectilesSpread * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(entityController.LookDirection, angle);
        }
        Debug.Log("ProjectileManager에게 발사체 생성 명령 호출");
    }

    private void CreateProjectile(Vector2 lookDirection, float angle)
    {
        projectileManager.ShootProjectile(
            this,
            projectileSpawnPosition.position,
            RotateVector2(lookDirection, angle));
    }

    private Vector2 RotateVector2(Vector2 vector, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * vector;
    }
}
