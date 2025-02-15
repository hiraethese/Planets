using UnityEngine;

public class PlanetBall : MonoBehaviour
{
    private Color _ballColor;
    private bool _isDestroyed = false;

    public void SetColor(Color color)
    {
        _ballColor = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("ShooterBall") || _isDestroyed)
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
            _isDestroyed = true;

            SoundManager.PlayDefaultClip();
            ShooterCounter.Instance.IncreaseGameScore();

            Destroy(gameObject);
        }
    }
}
