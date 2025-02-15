using UnityEngine;
using TMPro;

public class ShooterCounter : MonoBehaviour
{
    public static ShooterCounter Instance;
    public TextMeshProUGUI ballCountText;
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

    public bool GameEnd()
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
}
