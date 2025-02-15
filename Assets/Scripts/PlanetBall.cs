using UnityEngine;

public class PlanetBall : MonoBehaviour
{
    public AudioClip hitSoundClip;
    private AudioSource audioSource;
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

            if (shooter == null)
            {
                return;
            }

            Color shooterColor = shooter.GetShooterColor();

            if (shooterColor == ballColor)
            {
                audioSource = GetComponent<AudioSource>();
                audioSource.enabled = true;
                audioSource.clip = hitSoundClip;
                audioSource.Play();
                Destroy(gameObject);
            }
        }
    }
}
