using UnityEngine;

public class TriggerScript : MonoBehaviour
{
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
        huis.material = huismaterialdag;
        lantaarn.material = lantaarnmatdag;
        audiosource.Stop();
        audiosource.PlayOneShot(dagaudio);
        zon.SetActive(true);

        knop = false;

    }

    public void uit(){
        Debug.Log("ik ga uit");
        huis.material = huismaterialnacht;
        lantaarn.material = lantaarnmatnacht;
        audiosource.Stop();
        audiosource.PlayOneShot(nachtaudio);
        zon.SetActive(false);

        knop = true;

    }
}
