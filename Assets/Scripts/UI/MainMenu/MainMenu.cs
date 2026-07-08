using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Start")]
    public Button startButton;

    [Header("Options")]
    public Button openOptionsButton;
    public Button closeOptionsButton;
    public GameObject optionsPanel;

    [Header("Sound")]
    public Button openSoundButton;   // tombol di dalam Options panel
    public Button closeSoundButton;
    public GameObject soundPanel;

    [Header("Credits")]
    public Button openCreditsButton;
    public Button closeCreditsButton;
    public GameObject creditsPanel;

    [Header("Quit")]
    public Button quitButton;
    public Button quitYesButton;
    public Button quitNoButton;
    public GameObject quitPanel;

    void Start()
    {
        startButton.onClick.AddListener(LoadStartScene);

        openOptionsButton.onClick.AddListener(ShowOptionsPanel);
        closeOptionsButton.onClick.AddListener(HideOptionsPanel);

        openSoundButton.onClick.AddListener(ShowSoundPanel);
        closeSoundButton.onClick.AddListener(HideSoundPanel);

        openCreditsButton.onClick.AddListener(ShowCreditsPanel);
        closeCreditsButton.onClick.AddListener(HideCreditsPanel);

        quitButton.onClick.AddListener(ShowQuitPanel);
        quitYesButton.onClick.AddListener(QuitGame);
        quitNoButton.onClick.AddListener(HideQuitPanel);
    }

    void HideAllPanels()
    {
        optionsPanel.SetActive(false);
        soundPanel.SetActive(false);
        creditsPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void LoadStartScene() => SceneManager.LoadScene("Prolog");

    public void ShowOptionsPanel()
    {
        HideAllPanels();
        optionsPanel.SetActive(true);
    }
    public void HideOptionsPanel() => optionsPanel.SetActive(false);

    public void ShowSoundPanel()
    {
        optionsPanel.SetActive(false);
        soundPanel.SetActive(true);
    }
    public void HideSoundPanel()
    {
        soundPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ShowCreditsPanel()
    {
        HideAllPanels();
        creditsPanel.SetActive(true);
    }
    public void HideCreditsPanel() => creditsPanel.SetActive(false);

    public void ShowQuitPanel()
    {
        HideAllPanels();
        quitPanel.SetActive(true);
    }
    public void HideQuitPanel() => quitPanel.SetActive(false);

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }
}