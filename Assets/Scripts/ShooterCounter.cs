using UnityEngine;

public class ShooterCounter : MonoBehaviour
{
    public static ShooterCounter Instance;

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

    public void SetToMax()
    {
        currentBallCount = maxShooterBalls;
    }

    public void DecreaseBallCount()
    {
        currentBallCount--;
    }

    public bool EndTheGame()
    {
        return currentBallCount == 0;
    }
}
