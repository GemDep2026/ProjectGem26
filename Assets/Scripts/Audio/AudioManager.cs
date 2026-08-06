using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("- - - - - - - - Audio Source - - - - - - - -")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("- - - - - - - - Audio Clip - - - - - - - -")]
    public AudioClip background;
    public AudioClip walking;
    public AudioClip buttonClick;

    [Header("- - - - - - - - Slider - - - - - - - -")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("- - - - - - - - Mute Button - - - - - - - -")]
    [SerializeField] private Image muteButtonImage;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;

    private bool isMuted = false;

    private void Awake()
    {
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (musicSource != null && background != null)
        {
            musicSource.clip = background;
            musicSource.Play();
        }

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

    public void SetMusicVolume(float value) { musicSource.volume = value; }
    public void SetSfxVolume(float value) { sfxSource.volume = value; }

    public void PlaySfx(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // panggil dari Animation Event pas kaki nyentuh tanah
    public void PlayWalkingSfx() { PlaySfx(walking); }

    // panggil di OnClick() tombol UI
    public void PlayButtonSfx() { PlaySfx(buttonClick); }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;

        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        // UpdateMuteButton();
    }

    public void UpdateMuteButton()
    {
        if (muteButtonImage == null) return;
        muteButtonImage.sprite = isMuted ? musicOffSprite : musicOnSprite;
    }

    public bool IsMuted() { return isMuted; }
}