using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private Camera camera;

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
    }

    protected override void Start()
    {
        
    }

    
    protected override void Update()
    {
        base.Update();
    }

    // ------------ InputAction에서 ActionType을 모두 Value로 설정했기에 매개변수로 value를 가져옴

    // 모두 변수를 갱신하는 역할만을 한다. 
    private void OnMove(InputValue inputValue)
    {
        moveDirection = inputValue.Get<Vector2>();
    }

    private void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>(); // 마우스의 로컬 포지션을 가져옴
        Vector2 mouseWorldPosition = camera.ScreenToWorldPoint(mousePosition); // 마우스의 로컬 포지션을 메인 카메라의 월드 포지션으로 변환한다.
        lookDirection = (mouseWorldPosition - (Vector2)transform.position).normalized; // 마우스의 월드 포지션에 플레이어의 위치를 빼서 바라보는 방향을 정한다.
        // 이 때 normalized는 방향만 가져오고 길이는 1로 맞춰서 정규화해주는 역할이다. (굳이 길이가 필요없다)
            // 그럼 거리에 비례해서 데미지가 늘어나게 할 때는 정규화를 지우면 쓸 수 있을 것 같다.
                // 그런데 그러면 BaseController.LookRotate()에서 라디안에서 도로 변환하는 과정이 달라질 것 같다.
    }

    private void OnFire(InputValue inputValue)
    {
        Debug.Log("플레이어 공격 감지");
        isAttacking = inputValue.isPressed;
    }

    private void OnJump(InputValue inputValue)
    {

    }
}
