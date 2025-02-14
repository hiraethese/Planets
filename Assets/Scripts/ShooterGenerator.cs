using UnityEngine;

public class ShooterGenerator : MonoBehaviour
{
    public float shootForce = 15f;
    public Color[] ballColors = new Color[] {Color.red, Color.green, Color.blue};
    private Rigidbody rb;
    private bool isShot = false;

    void Start()
    {
        InitializeShooterBall();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBall();
        }
    }

    void InitializeShooterBall()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;
        rb.isKinematic = true;

        Renderer ballRenderer = GetComponent<Renderer>();
        if (ballRenderer != null && ballColors.Length > 0)
        {
            Material newMaterial = new Material(ballRenderer.material);
            Color selectedColor = ballColors[Random.Range(0, ballColors.Length)];
            newMaterial.SetColor("_BaseColor", selectedColor);
            ballRenderer.material = newMaterial;
        }
    }

    void ShootBall()
    {
        if (rb == null || isShot)
        {
            return;
        }

        isShot = true;

        rb.isKinematic = false;
        rb.useGravity = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 shootDirection;

        if (Physics.Raycast(ray, out hit))
        {
            shootDirection = (hit.point - transform.position).normalized;
        }
        else
        {
            shootDirection = Camera.main.transform.forward;
        }

        rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }
}
