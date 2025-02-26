using UnityEngine;

public class FlipTriggerScript : MonoBehaviour
{

    private FlipHallwayScript flipScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flipScript = GameObject.FindWithTag("Flip").GetComponent<FlipHallwayScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            flipScript.FlipHallway(collision.gameObject);
        }
    }
}
