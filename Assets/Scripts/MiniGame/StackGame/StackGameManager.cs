using UnityEngine;

public class StackGameManager : MonoBehaviour
{
    [SerializeField] private StackController stackController;
    public bool IsGameOver { get; private set; }
    public int combo { get; private set; } // 이번 판의 현재 콤보
    public int score { get; private set; } // 이번 판의 최고 점수
    public int bestScore { get; private set; } // 역대 최고 점수

    public int maxCombo { get; private set; }   // 이번 판의 최고 콤보
    public int bestCombo { get; private set; }  // 역대 최고 콤보

    private void Start()
    {
        stackController.OnBlockPlaced += HandleBlockPlaced;
        stackController.OnPerfectBlock += HandlePerfectBlock;
        stackController.OnGameOver += HandleGameOver;

        bestScore = PlayerPrefs.GetInt("StackBestScore", 0);
        bestCombo = PlayerPrefs.GetInt("StackBestCombo", 0);

        stackController.StopGame(); // 게임 시작시 UI가 먼저 띄워지는 동안 게임 진행이 되지 않도록
    }


    public void RestartGame()
    {
        IsGameOver = false;
        score = 0;
        combo = 0;
        maxCombo = 0;

        stackController.StartNewGame();
        Debug.Log("New Game Started!");
    }

    private void HandleBlockPlaced(int newScore)
    {
        score = newScore;
        combo = 0;
        Debug.Log($"Score: {score}");
        StackUIManager.Instance.UpdateScore();
    }

    private void HandlePerfectBlock(int newScore)
    {
        score = newScore;
        combo++;

        if (combo > maxCombo)
        {
            maxCombo = combo;
        }

        Debug.Log($"Score: {score}, Combo: {combo}!");
        StackUIManager.Instance.UpdateScore();
    }

    private void HandleGameOver()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        stackController.StopGame();
        Debug.Log("Game Over! Final Score: " + score);

        // 역대 최고 점수 갱신
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("StackBestScore", bestScore);
        }

        // 역대 최고 콤보 갱신
        if (maxCombo > bestCombo)
        {
            bestCombo = maxCombo;
            PlayerPrefs.SetInt("StackBestCombo", bestCombo);
        }
        StackUIManager.Instance.SetScoreUI();
    }
}