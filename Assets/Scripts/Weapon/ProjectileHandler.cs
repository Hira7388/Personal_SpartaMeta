using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ProjectileHandler : MonoBehaviour
{
    [Header("Projectile Stats")]
    [SerializeField] private float size = 1f;
    [SerializeField] private Color color = Color.white;
    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; }
    [SerializeField] private float lifeTime = 10f;
    public float LifeTime { get =>  lifeTime; }

    [Header("Projectile Component")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform pivot;

    private ProjectileController projectileController;

    private void Awake()
    {
        projectileController = GetComponent<ProjectileController>();
    }

    // 발사체 초기화
    public void Init(Vector2 projectileDirection, RangeWeaponHandler shootWeapon, ProjectileManager projectileManager)
    {
        // 외형 및 기본 설정
        transform.localScale = Vector3.one * size;
        spriteRenderer.color = color;

        // 발사 방향에 따라 오브젝트의 기본 방향(right)을 정렬
        transform.right = projectileDirection;

        // 발사 방향 x값이 음수(왼쪽)일 경우, 스프라이트의 상하를 뒤집어 방향을 맞춤
        if (projectileDirection.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        // 설정이 끝난 후, 해당 무기 컨트롤러에게 움직이라고 명령
        projectileController.Shoot(shootWeapon, projectileDirection, speed, lifeTime);
    }

    public void DestroyProjectile(Vector3 position, bool isCreateEffect)
    {
        if (isCreateEffect)
        {
            // 이펙트 생성 메서드(발사체 매니저에서 불러옴)
        }
        Destroy(this.gameObject);
    }
}
