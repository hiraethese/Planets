using System;
using UnityEngine;

public class PlanetBall : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitSoundClip;
    private Color ballColor;
    private bool isTouched = false;

    public void SetColor(Color color)
    {
        ballColor = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("ShooterBall") || isTouched)
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
            isTouched = true;

            Destroy(gameObject, hitSoundClip?.length ?? 0);
        }
    }
}
