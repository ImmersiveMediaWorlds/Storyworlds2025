using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject lantaarnlicht;

    public Renderer huis;
    public Material huismaterialdag;
    public Material huismaterialnacht;


    public Renderer lantaarn;
    public Material lantaarnmatdag;
    public Material lantaarnmatnacht;

    public AudioSource audiosource;
    public AudioClip dagaudio;
    public AudioClip nachtaudio;

    public GameObject zon;

    public GameObject maan;


    public bool knop;

    void Start()
    {
        knop = true;
    }


    void Update()
    {
        
    }

    public void CheckKnop(){




        if(knop == true){
            aan();
        }
        else{
            uit();
        }
    }


    public void aan(){
        Debug.Log("ik ga nu aan");
        lantaarnlicht.SetActive(false);
        huis.material = huismaterialdag;
        lantaarn.material = lantaarnmatdag;
        audiosource.Stop();
        audiosource.PlayOneShot(dagaudio);
        zon.SetActive(true);
        maan.SetActive(false);

        knop = false;

    }

    public void uit(){
        Debug.Log("ik ga uit");
        lantaarnlicht.SetActive(true);
        huis.material = huismaterialnacht;
        lantaarn.material = lantaarnmatnacht;
        audiosource.Stop();
        audiosource.PlayOneShot(nachtaudio);
        zon.SetActive(false);
        maan.SetActive(true);

        knop = true;

    }
}
