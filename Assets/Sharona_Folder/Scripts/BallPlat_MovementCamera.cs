using UnityEngine;

public class LookAtCameraAndMove : MonoBehaviour
{
    public Transform target; // De camera of ander doel
    public Transform[] destinations; // Meerdere bestemmingen
    public float speed = 5f; // Snelheid van beweging
    public float stopDistance = 0.1f; // Afstand waarop het object stopt
    public Vector3 rotationOffset = new Vector3(0, 180, 0); // Draai het object zodat de juiste kant naar de camera wijst
    public bool shouldLookAtCamera = true; // Kan aangepast worden wanneer het object stopt met kijken

    public AudioSource audioSource; // AudioSource voor geluidseffecten

    private int currentDestinationIndex = 0; // De index van de huidige bestemming waar het object naartoe beweegt
    private bool isMoving = true; // Is het object nog aan het bewegen?

    private bool isTriggered = false; // Houd bij of de trigger geactiveerd is

    void Update()
    {
        // Als er geen bestemmingen zijn, stop met het script
        if (destinations.Length == 0)
            return;

        // De huidige bestemming waar het object naartoe beweegt
        Transform currentDestination = destinations[currentDestinationIndex];

        // Bereken of het object dicht genoeg bij de bestemming is
        bool isAtDestination = Vector3.Distance(transform.position, currentDestination.position) <= stopDistance;

        // Als het object zich nog niet bij de bestemming bevindt, beweeg het er naartoe
        if (isMoving && !isAtDestination)
        {
            // Beweeg naar de bestemming
            transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, speed * Time.deltaTime);

            // Blijf naar de camera kijken zolang shouldLookAtCamera true is
            if (shouldLookAtCamera && target != null)
            {
                // Richt het object naar de camera
                Vector3 direction = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation * Quaternion.Euler(rotationOffset);
            }

            // Speel het geluid af zolang het object beweegt en het geluid niet al speelt
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (isAtDestination)
        {
            // Stop met bewegen zodra de bestemming bereikt is
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // Controleer of de trigger is geactiveerd
            if (isTriggered)
            {
                // Beweeg naar de volgende bestemming
                isMoving = true;
                isTriggered = false; // Reset de trigger voor de volgende bestemming
            }
            else
            {
                // Wacht op trigger om verder te bewegen
                isMoving = false; // Stop met bewegen totdat de trigger wordt geactiveerd
            }
        }
    }

    // Dit is de trigger die wordt geactiveerd wanneer de bal de triggerzone binnenkomt
    void OnTriggerEnter(Collider other)
    {
        // Controleer of de trigger een object is die je wilt gebruiken om de beweging voort te zetten
        if (other.CompareTag("Player")) // Of gebruik een andere tag die je wilt
        {
            isTriggered = true; // Zet de trigger actief
        }
    }
}


