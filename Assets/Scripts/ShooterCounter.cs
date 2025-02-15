using UnityEngine;
using TMPro;

public class ShooterCounter : MonoBehaviour
{
    public static ShooterCounter Instance;
    public TextMeshProUGUI ballCountText;
    public int maxShooterBalls = 50;
    private int currentBallCount;

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
        currentBallCount = maxShooterBalls;
        UpdateUI();
    }

    public void DecreaseBallCount()
    {
        if (currentBallCount > 0)
        {
            currentBallCount--;
            UpdateUI();
        }
    }

    public bool GameEnd()
    {
        return currentBallCount <= 0;
    }

    private void UpdateUI()
    {
        if (ballCountText != null)
        {
            ballCountText.text = "Balls: " + currentBallCount;
        }
    }
}
