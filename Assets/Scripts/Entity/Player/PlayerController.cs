using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    


    protected override void Start()
    {
        
    }

    
    protected override void Update()
    {
        base.Update();
    }

    // InputAction에서 ActionType을 모두 Value로 설정했기에 매개변수로 value를 가져옴
    private void OnMove(InputValue inputValue)
    {
        moveDirection = inputValue.Get<Vector2>();

    }

    private void OnLook(InputValue inputValue)
    {

    }

    private void OnFire(InputValue inputValue)
    {

    }

    private void OnJump(InputValue inputValue)
    {

    }
}
