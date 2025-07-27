using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // 캐릭터 객체별 리지드 바디를 가져오기 위한 변수

    [SerializeField] private SpriteRenderer characterRenderer; // 프리팹의 스프라이트
    [SerializeField] private Transform weaponPivot; // 프리팹의 무기 위치
    [SerializeField] private WeaponHandler weaponPrefab; // 가져올 프리팹 무기

    protected Vector2 moveDirection = Vector2.zero; // 캐릭터의 이동 방향
    public Vector2 MoveDirection { get { return moveDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // 캐릭터가 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } }

    protected bool isAttacking;
    private float timeSinceLastAttack = int.MaxValue;  // 0으로 초기화하면 첫 공격에 무기 Delay만큼 기다려야 한다

    // 컴포넌트 스크립트
    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;
    protected WeaponHandler weaponHandler;

    protected virtual void Awake()
    {
        // 스프라이트, 무기 트랜스폼은 인스펙터에서 받아오고
        // 리지드 바디, 애니메이션 핸들러, 스텟 핸들러는 같은 오브젝트에 컴포넌트로 붙어있기에 불러온다.
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
        if(weaponPrefab != null)
            weaponHandler = Instantiate(weaponPrefab, weaponPivot); // 프리팹이 있다면 무기 핸들러는 무기를 생성한다.
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>(); // 프리팹이 없다면 이미 장착중인 무기를 가져온다.
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        // 바라보기 메서드
        LookRotate(lookDirection);
        // 입력처리 메서드(공격)
        WeaponHandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        Movement(moveDirection);
    }

    // 내 이동 방향(매개변수)에 따라 이동하는 메서드
    private void Movement(Vector2 direction)
    {
        // 방향에다 속력을 곱해줘서 설정한 이동속보 변수에 따라 속도가 달라지게 한다.
        direction = direction * statHandler.MoveSpeed;
        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void LookRotate(Vector2 lookDirection)
    {
        // 라디안 값을 도 값으로 변환한다.
        // 도 =  라디안 * (180 / 파이)를 하면 된다.
        // 즉, Mathf.Rad2Deg가 180 / 파이 의 계산을 제공한다.
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(lookAngle) > 90;

        characterRenderer.flipX = isLeft;

        weaponPivot.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        weaponHandler?.WeaponRotate(isLeft); // 무기가 null이 아닐 때, 무기의 좌 우 회전
    }

    private void WeaponHandleAttackDelay()
    {
        if (weaponPrefab == null) // 무기가 없으면 동작하지 않음
            return;

        if (timeSinceLastAttack <= weaponHandler.AttackDelay) // 아직 무기 딜레이만큼 시간이 흐르지 않았다.
            timeSinceLastAttack += Time.deltaTime;

        // 디버그용
        //Debug.Log($"isAttacking: {isAttacking}, Time Since Attack: {timeSinceLastAttack}, Delay: {weaponHandler.AttackDelay}");

        if (isAttacking && timeSinceLastAttack  > weaponHandler.AttackDelay) // 무기 딜레이만큼 시간이 흘렀다.
        {
            timeSinceLastAttack = 0; // 다시 딜레이를 측정하기 위해 0초로 초기화
            Attack(); // 공격 시작
        }
    }

    private void Attack()
    {
        if(lookDirection != Vector2.zero)
        {
            Debug.Log("컨트롤러가 무기에게 공격 명령 호출");
            weaponHandler?.Attack();
        }
    }

    public virtual void Death()
    {
        Debug.LogError("!!! 캐릭터가 즉시 사망하고 있습니다 !!!");
        // 이하 생략...
    }
}
