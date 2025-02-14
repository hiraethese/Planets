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
            Renderer shooterRenderer = collision.gameObject.GetComponent<Renderer>();

            if (shooterRenderer != null)
            {
                Color shooterColor = shooterRenderer.material.GetColor("_BaseColor");

                if (shooterColor == ballColor)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
