using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    public GameObject planetBallPrefab;
    public float rotationSpeed = 10f;
    public int numberOfPlanetBalls = 100;
    public Color[] ballColors = new Color[] {Color.red, Color.green, Color.blue};
    private List<GameObject> planetBalls = new List<GameObject>();

    void Start()
    {
        GenerateBallsOnPlanet();
    }

    void Update()
    {
        RotatePlanet();
    }

    void GenerateBallsOnPlanet()
    {
        float planetRadius = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z) * 0.5f;

        for (int i = 0; i < numberOfPlanetBalls; i++)
        {
            Vector3 ballPosition = transform.position + Random.onUnitSphere * planetRadius;

            GameObject newBall = Instantiate(planetBallPrefab, ballPosition, Quaternion.identity, transform);

            Renderer ballRenderer = newBall.GetComponent<Renderer>();
            if (ballRenderer != null && ballColors.Length > 0)
            {
                Material newMaterial = new Material(ballRenderer.material);
                Color selectedColor = ballColors[Random.Range(0, ballColors.Length)];
                newMaterial.SetColor("_BaseColor", selectedColor);
                ballRenderer.material = newMaterial;
                newBall.AddComponent<PlanetBall>().SetColor(selectedColor);
            }

            Rigidbody ballRigidbody = newBall.GetComponent<Rigidbody>();
            if (ballRigidbody == null)
            {
                ballRigidbody = newBall.AddComponent<Rigidbody>();
            }
            ballRigidbody.isKinematic = true;

            planetBalls.Add(newBall);
        }
    }

    void RotatePlanet()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
