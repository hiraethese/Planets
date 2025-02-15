using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShooterCounter : MonoBehaviour
{
    public static ShooterCounter Instance;
    public TextMeshProUGUI ballCountText;
    public GameObject gameOverScreen;
    public int maxShooterBalls = 50;
    private int _currentBallCount;

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
        SetToMax();
    }

    public void SetToMax()
    {
        _currentBallCount = maxShooterBalls;
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
