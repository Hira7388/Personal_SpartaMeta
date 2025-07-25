using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // 캐릭터 객체별 리지드 바디를 가져오기 위한 변수

    [SerializeField] private SpriteRenderer characterRenderer; // 프리팹의 스프라이트
    [SerializeField] private Transform weaponPivot; // 프리팹의 무기 위치

    protected Vector2 moveDirection = Vector2.zero; // 캐릭터의 이동 방향
    public Vector2 MoveDirection { get { return moveDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // 캐릭터가 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } }

    // 컴포넌트 스크립트
    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;

    protected virtual void Awake()
    {
        // 스프라이트, 무기 트랜스폼은 인스펙터에서 받아오고
        // 리지드 바디, 애니메이션 핸들러, 스텟 핸들러는 같은 오브젝트에 컴포넌트로 붙어있기에 불러온다.
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        // 바라보기 메서드
        // 입력처리 메서드
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
    }
}
