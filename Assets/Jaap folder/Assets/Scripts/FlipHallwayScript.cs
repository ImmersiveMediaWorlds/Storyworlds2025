using UnityEngine;

public class FlipHallwayScript : MonoBehaviour
{

    [SerializeField] private GameObject hallway;
    [SerializeField] private GameObject player;
    private bool flipped = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipHallway(GameObject player)
    {
        if (flipped)
        {
            /* player exits beach area */
            hallway.transform.Rotate(0, 0, 0);
            // move the player to the other side of the hallway
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -player.transform.position.z);
            //rotate the player
            player.transform.Rotate(0, 180, 0);
            flipped = false;
        }
        else
        {
            /* player enters beach area */
            hallway.transform.Rotate(0, 180, 0);
            flipped = true;
        }
    }
}
