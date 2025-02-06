using UnityEngine;

public class LookAtCameraAndMove : MonoBehaviour
{
    public Transform target; // De camera of ander doel
    public Vector3 destination; // Doellocatie
    public float speed = 5f; // Snelheid van beweging
    public float stopDistance = 0.1f; // Afstand waarop hij stopt
    public Vector3 rotationOffset = new Vector3(0, 180, 0); // Draai het object 180° zodat de juiste kant naar de camera wijst

    void Update()
    {
        // Laat het object naar de camera kijken met een offset
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation * Quaternion.Euler(rotationOffset);
        }

        // Beweeg alleen als de afstand tot de bestemming groter is dan de stopafstand
        if (Vector3.Distance(transform.position, destination) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
    }
}
