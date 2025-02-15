using UnityEngine;

public class PlanetBall : MonoBehaviour
{
    private Color _ballColor;

    public void SetColor(Color color)
    {
        _ballColor = color;
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

        if (shooterColor == _ballColor)
        {
            SoundManager.PlayDefaultClip();

            Destroy(gameObject);
        }
    }
}
