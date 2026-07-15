using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("- - - - - - - - Audio Source - - - - - - - -")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    [Header("- - - - - - - - Audio Clip - - - - - - - -")]
    public AudioClip background;
    public AudioClip walking;

    [Header("- - - - - - - - Slider - - - - - - - -")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        if (musicSlider != null)
        {
            musicSlider.value = musicSource.volume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = sfxSource.volume;
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void SetSfxVolume(float value)
    {
        sfxSource.volume = value;
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}