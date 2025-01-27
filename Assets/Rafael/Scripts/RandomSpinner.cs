using UnityEngine;

public class RandomSpinner : MonoBehaviour
{
    public float initialSpeed = 10f;    // Initial spinning speed
    public float speedModifier = 0.1f;  // Speed increase factor per second
    public Vector3 spinAxis = Vector3.zero;  // Direction to spin on. If set to (0, 0, 0), will pick random direction

    private float currentSpeed;
    private Vector3 randomDirection;

    void Start()
    {
        // Initialize the current speed with the initial speed
        currentSpeed = initialSpeed;

        // If no axis is defined, pick a random axis
        if (spinAxis == Vector3.zero)
        {
            randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        else
        {
            randomDirection = spinAxis.normalized;
        }
    }

    void Update()
    {
        // Rotate the object around the random axis with current speed
        transform.Rotate(randomDirection, currentSpeed * Time.deltaTime);

        // Increase the speed over time
        currentSpeed += speedModifier * Time.deltaTime;
    }
}

