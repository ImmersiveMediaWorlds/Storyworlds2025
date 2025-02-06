using UnityEngine;

public class SetEnvActive : MonoBehaviour
{

    private GameObject player;
    public GameObject envoirement;
    [Header("Zet hier het nummer vande layer")]
    public int layer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            envoirement.layer = layer;

            foreach (Transform child in envoirement.transform)
            {
                child.gameObject.layer = layer;
            }
        }
    }

}
