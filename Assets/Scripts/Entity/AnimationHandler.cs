using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // 애니메이터에 있는 두 파라미터를 가져와서 StringToHash를 통해 숫자로 변환한다.
    // 문자열로 하는 것보다 숫자로 변환해서 사용하는 것이 휴먼 에러가 발생할 확률을 낮출 수 있다.
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    protected Animator animator;

    protected virtual void Awake()
    {
        // 이 스크립트는 Player 오브젝트에 붙히고 애니메이터는 그 하위에 있어서 Children을 붙힌다.
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {

    }

    // 어느 방향으로든 어느 속도 이상으로 움직이면 동작한다.
    public void Move(Vector2 entity)
    {
        // magnitude 는 벡터의 제곱근이다. 즉, 어느 방향으로 움직이던 그 크기가 0.5f 이상일 때 애니메이션이 동작하도록 한다.
        animator.SetBool(IsMove, entity.magnitude > 0.5f);
    }

    // 데미지를 입을 때 불러오면 된다.
    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    // 데미지 입는 딜레이 타임이 끝나고 불러온다.
    // 아직 어느 타이밍에 부르는지 모르겠다.
    public void DamageEnd()
    {
        animator.SetBool(IsDamage, false);
    }
}
