using System.Collections;
using UnityEngine;

public class EnvTouchScript : MonoBehaviour
{

    private GameObject player;
    private EnvironmentScript envScript;
    private FlipHallwayScript flipScript;

    private void Start()
    {
        player = GameObject.Find("Player");
        envScript = GameObject.FindWithTag("EnvironmentsHandler").GetComponent<EnvironmentScript>();
        flipScript = GameObject.FindWithTag("Flip").GetComponent<FlipHallwayScript>();
    }

    private bool isInsideTrigger = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == player)
        {
            if(transform.parent.gameObject.name == "Beach") flipScript.FlipHallway(player);
            isInsideTrigger = true;
            envScript.ActivateEnv(transform.parent.gameObject, true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        StartCoroutine(DelayedExitCheck(collision));
    }

    private IEnumerator DelayedExitCheck(Collider collision)
    {
        yield return new WaitForSeconds(0.2f); // Small delay
        if (collision.gameObject == player && isInsideTrigger) // Only exit if still outside after delay
        {
            isInsideTrigger = false;
            envScript.ActivateEnv(transform.parent.gameObject, false);
            Debug.Log("Exited " + transform.parent.gameObject.name);
        }
    }

}
