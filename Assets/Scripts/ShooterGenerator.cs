using UnityEngine;

public class ShooterGenerator : MonoBehaviour
{
    public float shootForce = 15f;
    public float ballLifetime = 5f;
    public GameObject shooterBallPrefab;
    public Color[] ballColors = new Color[] {Color.red, Color.green, Color.blue};
    private Rigidbody rb;
    private bool isShot = false;
    private bool canRespawn = true;
    private float timer = 0f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        InitializeBall();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isShot)
        {
            ShootBall();
        }

        if (isShot)
        {
            timer += Time.deltaTime;
        }

        if ( (transform.position.y < -20f || timer >= ballLifetime) && canRespawn )
        {
            canRespawn = false;
            RespawnBall();
        }
    }

    void InitializeBall()
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

        timer = 0f;
        canRespawn = true;
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

    void RespawnBall()
    {
        Destroy(gameObject, 0.1f);

        Instantiate(shooterBallPrefab, initialPosition, Quaternion.identity);
    }
}
