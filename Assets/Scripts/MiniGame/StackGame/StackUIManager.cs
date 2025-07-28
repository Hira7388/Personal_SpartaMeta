using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIState
{
    Home,
    Game,
    Score
}

public class StackUIManager : MonoBehaviour
{
    public static StackUIManager Instance { get; private set; }

    UIState currentState = UIState.Home;
    StackHomeUI homeUI = null;
    StackGameUI gameUI = null;
    StackScoreUI scoreUI = null;

    [SerializeField] StackGameManager stack;

    private void Awake()
    {
        Instance = this;

        homeUI = GetComponentInChildren<StackHomeUI>(true);
        // true 매개변수의 역할은 비활성화된 오브젝트도 찾을 것인지 물어본다.
        homeUI?.Init(this);

        gameUI = GetComponentInChildren<StackGameUI>(true);
        gameUI?.Init(this);

        scoreUI = GetComponentInChildren<StackScoreUI>(true);
        scoreUI?.Init(this);
    }

    private void Start()
    {
        ChangeState(UIState.Home);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    public void OnClickStart()
    {
        stack.RestartGame();
        ChangeState(UIState.Game);
    }

    public void OnClickExit()
    {
        GameManager.Instance.LoadSceneWithFade("MainScene");
    }

    public void UpdateScore()
    {
        gameUI.SetUI(stack.score, stack.combo, stack.maxCombo);
    }

    public void SetScoreUI()
    {
        scoreUI.SetUI(stack.score, stack.maxCombo, stack.bestScore, stack.bestCombo);
        ChangeState(UIState.Score);
    }
}
