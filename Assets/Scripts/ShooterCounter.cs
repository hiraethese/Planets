using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShooterCounter : MonoBehaviour
{
    public static ShooterCounter Instance;
    public TextMeshProUGUI ballCountText;
    public TextMeshProUGUI gameScoreText;
    public GameObject gameOverScreen;
    public int maxShooterBalls = 50;
    private int _currentBallCount;
    private int _currentGameScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetGameScoreToZero();
        SetBallCountToMax();
    }

    public void SetGameScoreToZero()
    {
        _currentGameScore = 0;
        UpdateUI();
    }

    public void SetBallCountToMax()
    {
        _currentBallCount = maxShooterBalls;
        UpdateUI();
    }

    public void IncreaseGameScore()
    {
        _currentGameScore++;
        UpdateUI();
    }

    public void DecreaseBallCount()
    {
        if (_currentBallCount > 0)
        {
            _currentBallCount--;
            UpdateUI();
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public bool IsGameEnd()
    {
        return _currentBallCount <= 0;
    }

    private void UpdateUI()
    {
        if (gameScoreText != null)
        {
            gameScoreText.text = "Score: " + _currentGameScore;
        }

        if (ballCountText != null)
        {
            ballCountText.text = "Balls: " + _currentBallCount;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
