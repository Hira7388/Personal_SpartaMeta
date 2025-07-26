using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileController : MonoBehaviour
{
    // 충돌하면 발사체를 삭제할 레이어
    [SerializeField] private LayerMask levelCollisionLayer;

    // 발사하는 무기 (발사체 매니저에서 전달해줌)
    private RangeWeaponHandler rangeWeaponHandler;
    // 발사체 매니저
    private ProjectileManager projectileManager;

    // 발사체 현재 생존시간
    private float currentLifeTime;
    // 발사체 생성이 완료되서 이동할 준비가 되었는지 확인
    private bool isShootingReady;

    // Handler에서 전달받을 데이터
    private Vector2 projectileDirection;
    private float projectileSpeed;
    private float projectileLifeTime;

    private Rigidbody2D _rigidbody;
    private ProjectileHandler _projectileHandler;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _projectileHandler = GetComponent<ProjectileHandler>();
    }


    private void Update()
    {
        // 아직 발사체의 이동 준비가 완료되지 않았다.
        if (!isShootingReady)
            return;

        // 발사체 생존 시간이 다 지나면 삭제
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= _projectileHandler.LifeTime)
        {
            _projectileHandler.DestroyProjectile(transform.position, false);
        }

        // 발사체 이동
        _rigidbody.velocity = projectileDirection * _projectileHandler.Speed;
    }

    public void Shoot(RangeWeaponHandler weapon, Vector3 direction, float speed, float lifeTime)
    {
        this.rangeWeaponHandler = weapon;
        projectileDirection = direction.normalized;
        projectileSpeed = speed;
        projectileLifeTime = lifeTime;

        currentLifeTime = 0f;
        // 쏠 수 있어~!
        isShootingReady = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 설정한 충돌 레이어와 실제 충돌한 레이어가 같은지 확인
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            _projectileHandler.DestroyProjectile(collision.ClosestPoint(transform.position) - projectileDirection * 0.2f, true);
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            // 데미지 처리를 진행한다.
        }
    }
}
