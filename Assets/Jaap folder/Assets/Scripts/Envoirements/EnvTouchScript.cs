using UnityEngine;

public class EnvTouchScript : MonoBehaviour
{

    private GameObject player;
    private EnvironmentSwapScript envScript;

    private void Start()
    {
        player = GameObject.Find("Player");
        envScript = GameObject.FindWithTag("EnvironmentsHandler").GetComponent<EnvironmentSwapScript>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == player)
        {
            envScript.ActivateEnv(transform.parent.gameObject, true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject == player)
        {
            envScript.ActivateEnv(transform.parent.gameObject, false);
        }
    }
}
