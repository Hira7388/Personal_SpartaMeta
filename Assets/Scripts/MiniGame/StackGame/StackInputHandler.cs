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
        Debug.Log($"[Input Check] GameManager: {gameManager != null}, IsGameOver: {gameManager?.IsGameOver}, isPressed: {value.isPressed}, StackController: {stackController != null}");

        if (gameManager != null && !gameManager.IsGameOver && value.isPressed)
        {
            if (stackController != null)
            {
                Debug.Log("조건 통과! PlaceBlock()를 호출합니다.");
                stackController.PlaceBlock();
            }
            else
            {
                Debug.LogError("stackController가 연결되지 않았습니다!");
            }
        }
    }
}
