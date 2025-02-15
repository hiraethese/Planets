using System.Collections.Generic;
using UnityEngine;

public class ShooterGenerator : MonoBehaviour
{
    public float shootForce = 15f;
    public float ballLifetime = 5f;
    public GameObject shooterBallPrefab;
    public Color[] ballColors = new Color[] {Color.red, Color.green, Color.blue};
    public LineRenderer trajectoryLine;
    public int trajectoryPoints = 20;
    public float timeStep = 0.1f;
    private Rigidbody _rb;
    private bool _isShot = false;
    private bool _canRespawn = true;
    private float _timer = 0f;
    private Vector3 _initialPosition;
    private Color _ballColor;

    void Start()
    {
        _initialPosition = transform.position;
        InitializeBall();
        trajectoryLine.enabled = false;
    }

    void Update()
    {
        if (!_isShot)
        {
            if (Input.GetMouseButton(0))
            {
                DrawTrajectory();
            }
            if (Input.GetMouseButtonUp(0))
            {
                ShootBall();
                ClearTrajectory();
            }
        }

        if (_isShot)
        {
            _timer += Time.deltaTime;
        }

        if ( (transform.position.y < -20f || _timer >= ballLifetime) && _canRespawn )
        {
            _canRespawn = false;
            RespawnBall();
        }
    }

    void InitializeBall()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody>();
        }

        _rb.useGravity = false;
        _rb.isKinematic = true;

        Renderer ballRenderer = GetComponent<Renderer>();
        if (ballRenderer != null && ballColors.Length > 0)
        {
            Material newMaterial = new Material(ballRenderer.material);
            Color selectedColor = ballColors[Random.Range(0, ballColors.Length)];
            newMaterial.SetColor("_BaseColor", selectedColor);
            ballRenderer.material = newMaterial;
            _ballColor = selectedColor;
        }

        _timer = 0f;
        _canRespawn = true;
    }

    void ShootBall()
    {
        if (_rb == null || _isShot)
        {
            return;
        }

        _isShot = true;

        _rb.isKinematic = false;
        _rb.useGravity = true;

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

        _rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }

    void DrawTrajectory()
    {
        trajectoryLine.enabled = true;
        List<Vector3> points = new List<Vector3>();

        Vector3 startPosition = transform.position;
        Vector3 velocity = GetShootDirection() * shootForce;
        Vector3 gravity = Physics.gravity;

        Vector3 previousPoint = startPosition;
        points.Add(previousPoint);

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeStep;
            Vector3 point = startPosition + velocity * t + 0.5f * gravity * t * t;

            RaycastHit hit;
            if (Physics.Raycast(previousPoint, point - previousPoint, out hit, Vector3.Distance(previousPoint, point)))
            {
                points.Add(hit.point);
                break;
            }

            points.Add(point);
            previousPoint = point;
        }

        trajectoryLine.positionCount = points.Count;
        trajectoryLine.SetPositions(points.ToArray());
    }

    void ClearTrajectory()
    {
        trajectoryLine.enabled = false;
        trajectoryLine.positionCount = 0;
    }

    Vector3 GetShootDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return (hit.point - transform.position).normalized;
        }
        return Camera.main.transform.forward;
    }

    void RespawnBall()
    {
        if (ShooterCounter.Instance != null)
        {
            ShooterCounter.Instance.DecreaseBallCount();
        }

        Destroy(gameObject, 0.1f);

        if (ShooterCounter.Instance.IsGameEnd())
        {
            ShooterCounter.Instance.GameOver();
            return;
        }

        Instantiate(shooterBallPrefab, _initialPosition, Quaternion.identity);
    }

    public Color GetShooterColor()
    {
        return _ballColor;
    }
}
