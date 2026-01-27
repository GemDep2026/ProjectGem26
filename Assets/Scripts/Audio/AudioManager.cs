using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("- - - - - - - - Audio Source - - - - - - - -")]
    public AudioSource musicSource;
    public AudioSource sfxSource;


    [Header("- - - - - - - - Audio Clip - - - - - - - -")]
    public AudioClip background;
    public AudioClip walking;
    //public AudioClip moveScene;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
