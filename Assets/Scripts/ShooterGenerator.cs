using UnityEngine;

public class ShooterGenerator : MonoBehaviour
{
    public Color[] ballColors = new Color[] {Color.red, Color.green, Color.blue};

    void Start()
    {
        InitializeShooterBall();
    }

    void InitializeShooterBall()
    {
        Renderer ballRenderer = GetComponent<Renderer>();
        if (ballRenderer != null && ballColors.Length > 0)
        {
            Material newMaterial = new Material(ballRenderer.material);
            Color selectedColor = ballColors[Random.Range(0, ballColors.Length)];
            newMaterial.SetColor("_BaseColor", selectedColor);
            ballRenderer.material = newMaterial;
        }

        Rigidbody ballRigidbody = GetComponent<Rigidbody>();
        if (ballRigidbody == null)
        {
            ballRigidbody = gameObject.AddComponent<Rigidbody>();
        }
        ballRigidbody.useGravity = true;
        ballRigidbody.isKinematic = false;
    }
}
