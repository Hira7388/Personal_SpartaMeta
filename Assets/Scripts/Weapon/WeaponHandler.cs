using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] protected float attackDelay = 1f; // 공격 속도
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }

    [SerializeField] protected float weaponSize = 1f; // 무기의 크기
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] protected float attackPower = 1f; // 무기의 데미지(탄의 데미지는 따로)
    public float WeaponPower { get => attackPower; set => attackPower = value; }

    public LayerMask target; // 어떤 오브젝트 레이어를 타겟으로 할지

    private static readonly int IsAttack = Animator.StringToHash("isAttack"); // 애니메이터에 IsAttack 파라미터를 숫자로 변환


    // 컴포넌트 가져오기
    public BaseController entityController { get; private set; } // 무기를 장착하는 엔티티 가져오기
    private Animator animator; // 자식 오브젝트에 있다
    private SpriteRenderer weaponRenderer; // 자식 오브젝트에 무기 렌더러

    protected virtual void Awake()
    {
        entityController = GetComponentInParent<BaseController>(); // 무기는 엔티티의 하위 오브젝트로 생성된다.
        animator = GetComponentInChildren<Animator>(); // 자식 오브젝트의 애니메이터
        weaponRenderer = GetComponentInChildren<SpriteRenderer>(); // 자식 오브젝트의 스프라이트 렌더러

        animator.speed = 1.0f / attackDelay; // 내 공격속도와 애니메이션을 연동함
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    // 공격 로직
    public virtual void Attack()
    {
        AttackAnimation(); // 공격 애니메이션 호출
    }

    // 공격 애니메이션
    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    // 무기도 엔티티의 상태에 맞춰서 뒤집혀야 한다.
    // BaseController에 LookRotate()에서 호출
    public void WeaponRotate(bool isLeft)
    {
        // 캐릭터의 뒤집힘 변수를 매개변수로 전달해줘야 할듯
        weaponRenderer.flipY = isLeft; 
    }
}
