using UnityEngine;

public class LookAtCameraAndMove : MonoBehaviour
{
    public Transform target; // De camera of ander doel
    public Vector3 destination; // Doellocatie
    public float speed = 5f; // Snelheid van beweging
    public float stopDistance = 0.1f; // Afstand waarop hij stopt
    public Vector3 rotationOffset = new Vector3(0, 180, 0); // Draai het object 180° zodat de juiste kant naar de camera wijst
    public bool shouldLookAtCamera = true; // Kan aangepast worden wanneer het object stopt met kijken

    public AudioSource audioSource; // Sleep hier een AudioSource in de Inspector

    void Update()
    {
        bool isMoving = Vector3.Distance(transform.position, destination) > stopDistance;

        // Alleen naar de camera kijken als shouldLookAtCamera true is
        if (target != null && shouldLookAtCamera)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation * Quaternion.Euler(rotationOffset);
        }

        // Beweeg en speel geluid af als het object nog niet op de bestemming is
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            // Speel het geluid alleen als het nog niet speelt
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            shouldLookAtCamera = false; // Stop met draaien zodra de bestemming bereikt is

            // Stop het geluid zodra het object stilstaat
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}

