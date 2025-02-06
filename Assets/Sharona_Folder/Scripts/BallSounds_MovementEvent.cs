using UnityEngine;

public class PlaySoundOnEvent : MonoBehaviour
{
    public AudioSource audioSource; // Sleep een AudioSource hierheen
    public AudioClip soundClip; // Sleep je geluid in de Inspector

    public void PlaySound() // LET OP: Deze functie moet 'public' zijn!
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }
}
