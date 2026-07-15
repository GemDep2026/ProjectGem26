using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("- - - - - - - - Panel - - - - - - - -")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject audioMenuUI;
    [SerializeField] private GameObject settingMenuUI;

    private bool isPaused;
    private bool isAudioOpen;
    private bool isSettingOpen;

    public bool TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenuUI.SetActive(isPaused);
        return isPaused;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);

        // pastikan panel anak ikut ketutup pas resume
        isAudioOpen = false;
        isSettingOpen = false;
        audioMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
    }

    public bool ToggleAudioMenu()
    {
        isAudioOpen = !isAudioOpen;
        audioMenuUI.SetActive(isAudioOpen);
        return isAudioOpen;
    }

    public void CloseAudioMenu()
    {
        isAudioOpen = false;
        audioMenuUI.SetActive(false);
    }

    public bool ToggleSettingMenu()
    {
        isSettingOpen = !isSettingOpen;
        settingMenuUI.SetActive(isSettingOpen);
        return isSettingOpen;
    }

    public void CloseSettingMenu()
    {
        isSettingOpen = false;
        settingMenuUI.SetActive(false);
    }
}