using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("Fade Settings")]
    public CanvasGroup blackScreen;
    public float fadeDuration = 1f;

    [Header("Start Scene Fade")]
    public bool fadeFromBlackOnStart = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (fadeFromBlackOnStart)
        {
            StartCoroutine(FadeFromBlack());
        }
        else
        {
            blackScreen.alpha = 0f;
        }
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    IEnumerator FadeAndLoad(string sceneName)
    {
        yield return StartCoroutine(FadeToBlack());

        SceneManager.LoadScene(sceneName);

        yield return null;

        if (fadeFromBlackOnStart)
        {
            StartCoroutine(FadeFromBlack());
        }
    }

    IEnumerator FadeToBlack()
    {
        blackScreen.gameObject.SetActive(true);

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            blackScreen.alpha =
                Mathf.Lerp(0f, 1f, time / fadeDuration);

            yield return null;
        }

        blackScreen.alpha = 1f;
    }

    IEnumerator FadeFromBlack()
    {
        blackScreen.gameObject.SetActive(true);

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            blackScreen.alpha =
                Mathf.Lerp(1f, 0f, time / fadeDuration);

            yield return null;
        }

        blackScreen.alpha = 0f;
    }
}