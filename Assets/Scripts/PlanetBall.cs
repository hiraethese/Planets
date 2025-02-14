using UnityEngine;

public class PlanetBall : MonoBehaviour
{
    private Color ballColor;

    public void SetColor(Color color)
    {
        ballColor = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ShooterBall"))
        {
            ShooterGenerator shooter = collision.gameObject.GetComponent<ShooterGenerator>();

            if (shooter != null)
            {
                Color shooterColor = shooter.GetShooterColor();

                if (shooterColor == ballColor)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
