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
        if (!collision.gameObject.CompareTag("ShooterBall"))
        {
            return;
        }

        ShooterGenerator shooter = collision.gameObject.GetComponent<ShooterGenerator>();

        if (shooter == null)
        {
            return;
        }

        Color shooterColor = shooter.GetShooterColor();

        if (shooterColor == ballColor)
        {
            SoundManager.PlayDefaultClip();

            Destroy(gameObject);
        }
    }
}
