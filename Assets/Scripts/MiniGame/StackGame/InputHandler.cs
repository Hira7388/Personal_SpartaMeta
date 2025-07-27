using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour
{
    [SerializeField] private StackController stackController;
    [SerializeField] private StackGameManager gameManager;

    private void OnPlaceBlock(InputValue value)
    {
        // 어떤 스크립트가, 어떤 게임오브젝트에서 신호를 받았는지 정확히 출력
        Debug.Log("Input from Script: " + this.GetType().Name, this.gameObject);
        if (gameManager != null && !gameManager.IsGameOver && value.isPressed)
            stackController?.PlaceBlock();
    }
}
