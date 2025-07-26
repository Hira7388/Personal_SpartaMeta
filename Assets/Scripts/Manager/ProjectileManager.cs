using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    /* 역할
    모든 발사체를 관리하는 역할,
    추후 오브젝트 풀링을 사용할 때 좋다고 한다,
    발사체를 생성, 이펙트를 담당
    발사체의 충돌 처리는 각 발사체가 직접 담당한다.
    */

    private static ProjectileManager instance;
    public static ProjectileManager Instance { get => instance; }
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private ParticleSystem impactParticleSystem;

    private void Awake()
    {
        instance = this;
    }


    // 발사체를 발사하는 메서드
    public void ShootProjectile(RangeWeaponHandler projectileShootWeapon, Vector2 projectileStartPosition, Vector2 projectileDirection)
    {
        // 발사하는 무기에 설정된 발사체 번호에 맞는 프리팹을 저장한다.
        GameObject projectilePrefab = projectilePrefabs[projectileShootWeapon.ProjectileIndex];
        // 발사체 인스턴스를 생성한다.
        GameObject projectile = Instantiate(projectilePrefab, projectileStartPosition, Quaternion.identity);

        // 생성한 발사체 인스턴스의 발사체 컨트롤러 컴포넌트를 가져온다.
        ProjectileHandler projectileHandler = projectile.GetComponent<ProjectileHandler>();
        // 발사체를 초기화 한다.
        projectileHandler.Init(projectileDirection, projectileShootWeapon, this);
    }

    // 발사체의 충돌 이펙트를 생성하는 메서드
    public void CreateEffectProjectileAtPosition()
    {

    }
}
