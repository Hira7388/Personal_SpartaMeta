using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour
{
    [SerializeField] private StackController stackController;
    [SerializeField] private StackGameManager gameManager;

    private bool blockPlacementRequested = false;

    private void OnPlaceBlock(InputValue value)
    {
        // 입력이 들어왔다고만 알리는 역할
        blockPlacementRequested = true;
    }

    private void Update()
    {
        if (!blockPlacementRequested)
        {
            return;
        }

        blockPlacementRequested = false;

        // UI 클릭은 무시
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("UI를 클릭했습니다.");
            return;
        }

        // 게임 실행
        if (gameManager != null && !gameManager.IsGameOver)
        {
            stackController?.PlaceBlock();
        }
    }
}
