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

    [Header("- - - - - - - - Mute Button - - - - - - - -")]
    [SerializeField] private Image muteButtonImage;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;

    private bool isMuted = false;
    private bool isMusicMuted = false;

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

        UpdateMuteButton();
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

    public void ToggleMute()
    {
        isMuted = !isMuted;

        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;

        UpdateMuteButton();
    }

    private void UpdateMuteButton()
    {
        if (muteButtonImage == null) return;

        muteButtonImage.sprite = isMuted
            ? musicOffSprite
            : musicOnSprite;
    }

    public bool IsMusicMuted()
    {
        return isMusicMuted;
    }
}