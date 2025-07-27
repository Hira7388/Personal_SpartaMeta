using UnityEngine;

public class StackGameManager : MonoBehaviour
{
    [SerializeField] private StackController stackController;
    public bool IsGameOver { get; private set; }
    private int score;
    private int combo;
    private int bestScore;

    private void Start()
    {
        stackController.OnBlockPlaced += HandleBlockPlaced;
        stackController.OnPerfectBlock += HandlePerfectBlock;
        stackController.OnGameOver += HandleGameOver;
        bestScore = PlayerPrefs.GetInt("StackBestScore", 0);
        RestartGame();
    }

    public void RestartGame()
    {
        IsGameOver = false;
        score = 0;
        combo = 0;
        stackController.StartNewGame();
        Debug.Log("New Game Started!");
    }

    private void HandleBlockPlaced(int newScore)
    {
        score = newScore;
        combo = 0;
        Debug.Log($"Score: {score}");
    }

    private void HandlePerfectBlock(int newScore)
    {
        score = newScore;
        combo++;
        Debug.Log($"Score: {score}, Combo: {combo}!");
    }

    private void HandleGameOver()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        stackController.StopGame();
        Debug.Log("Game Over! Final Score: " + score);
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("StackBestScore", bestScore);
        }
    }
}