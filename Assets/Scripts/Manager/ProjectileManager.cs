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

    private void Awake()
    {
        instance = this;
    }

    // 발사체를 발사하는 메서드
    public void ShootProjectile()
    {

    }

    // 발사체의 충돌 이펙트를 생성하는 메서드
    public void CreateEffectProjectileAtPosition()
    {

    }
}
