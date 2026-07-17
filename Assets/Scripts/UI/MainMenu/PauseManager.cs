using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject settingsPanelsParent;

    private bool isPaused;

    private void Start()
    {
        pausePanel.SetActive(false);
        settingsPanelsParent.SetActive(false);
        settingsPanel.SetActive(false);
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
}