using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject settingsPanelsParent;

    [SerializeField] private Image muteButtonImage;
    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;

    private bool isPaused;

    private void Start()
    {
        pausePanel.SetActive(false);
        settingsPanelsParent.SetActive(false);
        settingsPanel.SetActive(false);
        
        UpdateMuteIcon();
    }

    // Tombol Pause
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pausePanel.SetActive(isPaused);

        if (!isPaused)
        {
            settingsPanel.SetActive(false);
            settingsPanelsParent.SetActive(false);
        }
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        settingsPanel.SetActive(false);
        settingsPanelsParent.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanelsParent.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        settingsPanelsParent.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void PlaySFX()
    {
        if (AudioManager.Instance != null && AudioManager.Instance.sfxSource != null && AudioManager.Instance.buttonClick != null)
        {
            AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.buttonClick);
        }
    }

    public void MuteMusic()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMute();
            UpdateMuteIcon();
        }
    }
    
    private void UpdateMuteIcon()
    {
        muteButtonImage.sprite = AudioManager.Instance.IsMuted()
            ? musicOff
            : musicOn;
    }
}