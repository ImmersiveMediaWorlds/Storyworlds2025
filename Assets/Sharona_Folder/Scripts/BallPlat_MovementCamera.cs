using UnityEngine;
using System.Collections;

public class LookAtCameraAndMove : MonoBehaviour
{
    public Transform target; // De camera (VR-headset)
    public Transform[] destinations; // Meerdere bestemmingen
    public float speed = 5f; // Snelheid van beweging
    public float stopDistance = 0.1f; // Afstand waarop het object stopt
    public Vector3 rotationOffset = new Vector3(0, 180, 0); // Draai het object zodat de juiste kant naar de camera wijst
    public AudioSource audioSource; // AudioSource voor geluidseffecten

    public LayerMask gazeLayer; // Alleen objecten op deze layer reageren op gaze
    public float gazeTimeRequired = 2f; // Hoe lang moet de speler kijken?

    private int currentDestinationIndex = 0; // Index van de huidige bestemming
    private bool isMoving = true; // Beweegt het object?
    private bool isGazing = false; // Kijkt de speler momenteel naar de trigger?
    private Coroutine gazeCoroutine; // Houdt de gaze-timer bij
    private bool isLastDestination = false; // Kijken of de bal bij de laatste bestemming is

    private bool hasLoggedLastDestination = false; // Het stoppen van een debug log.

    public float gazeTriggerDistance = 3f; // Minimale afstand voordat gaze werkt

    void Update()
    {
        if (destinations.Length == 0)
        {
            return;
        }

        Transform currentDestination = destinations[currentDestinationIndex];
        float distanceToDestination = Vector3.Distance(transform.position, currentDestination.position);
        bool isAtDestination = distanceToDestination <= stopDistance;

        if (isMoving && !isAtDestination)
        {
            // Beweeg naar de bestemming
            transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, speed * Time.deltaTime);


            if (!isLastDestination) // Alleen draaien als het NIET de laatste bestemming is
            {
                if (target != null)
                {
                    Vector3 direction = target.position - transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = lookRotation * Quaternion.Euler(rotationOffset);
                }
            }

            // Speel geluid af als het object beweegt
            if (!audioSource.isPlaying) audioSource.Play();
        }
        else if (isAtDestination)
        {
            if (currentDestinationIndex >= destinations.Length - 1 && !hasLoggedLastDestination)
            {
                Debug.Log("Laatste bestemming bereikt!");
                isLastDestination = true;
                hasLoggedLastDestination = true;
                return;
            }

            if (audioSource.isPlaying) audioSource.Stop();

            if (target != null && !isLastDestination)
            {
                Vector3 direction = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = lookRotation * Quaternion.Euler(rotationOffset);
            }

            // ✅ Controleer of de speler binnen de juiste afstand is voordat gaze werkt
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);
            if (distanceToPlayer <= gazeTriggerDistance)
            {
                CheckForGazeTrigger(); // Alleen starten als de speler dichtbij genoeg is!
            }
        }

    }

        void CheckForGazeTrigger()
    {
        Ray ray = new Ray(target.position, target.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, gazeLayer))
        {
            if (!isGazing)
            {
                isGazing = true;
                gazeCoroutine = StartCoroutine(GazeTriggerTimer());
            }
        }
        else
        {
            if (isGazing)
            {
                isGazing = false;
                if (gazeCoroutine != null)
                {
                    StopCoroutine(gazeCoroutine);
                }
            }
        }
    }

    IEnumerator GazeTriggerTimer()
    {
        yield return new WaitForSeconds(gazeTimeRequired);

        if (isGazing) // Controleer of de speler nog steeds kijkt
        {
            GoToNextDestination();
        }
    }

    void GoToNextDestination()
    {
        currentDestinationIndex++;

        if (currentDestinationIndex >= destinations.Length)
        {
            return; // Stop als alle bestemmingen bereikt zijn
        }

        isMoving = true;
    }
}








