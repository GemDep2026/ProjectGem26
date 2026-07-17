using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Start")]
    public Button startButton;

    [Header("Sound")]
    public Button openSoundButton;
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

    private void Start()
    {
        HideAllPanels();

        startButton.onClick.AddListener(LoadStartScene);


        openSoundButton.onClick.AddListener(ShowSoundPanel);
        closeSoundButton.onClick.AddListener(HideAllPanels);

        openCreditsButton.onClick.AddListener(ShowCreditsPanel);
        closeCreditsButton.onClick.AddListener(HideAllPanels);

        quitButton.onClick.AddListener(ShowQuitPanel);
        quitYesButton.onClick.AddListener(QuitGame);
        quitNoButton.onClick.AddListener(HideAllPanels);
    }

    private void HideAllPanels()
    {
        soundPanel.SetActive(false);
        creditsPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("Prolog");
    }

    public void ShowOptionsPanel()
    {
        HideAllPanels();
    }

    public void ShowSoundPanel()
    {
        HideAllPanels();
        soundPanel.SetActive(true);
    }

    public void ShowCreditsPanel()
    {
        HideAllPanels();
        creditsPanel.SetActive(true);
    }

    public void ShowQuitPanel()
    {
        HideAllPanels();
        quitPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }
}