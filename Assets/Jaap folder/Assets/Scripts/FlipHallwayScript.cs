using UnityEngine;

public class FlipHallwayScript : MonoBehaviour
{

    [SerializeField] private GameObject hallway;
    [SerializeField] private GameObject flipTrigger;
    [SerializeField] private GameObject fakeBeach;
    [SerializeField] private GameObject gordijn;
    [SerializeField] private Animator gordijnAnimator;
    private bool flipped = false;

    public void Start()
    {

    }

    public void FlipHallway(GameObject player)
    {
        if (flipped)
        {
            /* player exits beach area */
            hallway.transform.Rotate(0, -180, 0);
            // move the player to the other side of the hallway
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -player.transform.position.z);
            //rotate the player
            player.transform.Rotate(0, 180, 0);
            flipped = false;
            flipTrigger.SetActive(false);
            fakeBeach.SetActive(false);
            gordijn.SetActive(true);
            gordijnAnimator.SetBool("gordijnTrigger", true);
        }
        else 
        {
            /* player enters beach area */
            hallway.transform.Rotate(0, 180, 0);
            flipped = true;
            flipTrigger.SetActive(true);
            fakeBeach.SetActive(true);
            gordijn.SetActive(false);
        }
    }
}