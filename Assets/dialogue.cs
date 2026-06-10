using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dialogue : MonoBehaviour
{
    [Header("Ink JSON File")]
    public TextAsset inkJSON;

    [Header("UI")]
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    [Header("Typing Settings")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Next Scene")]
    [SerializeField] private string nextSceneName;

    [Header("Player Settings")]
    public NpcMovement npcMovement;
    public int[] guideTargetSteps;

    [Header("Fade Transition")]
    [SerializeField] private bool useFadeTransition = true;
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private float fadeDuration = 1f;


    private Story story;
    private Coroutine typingCoroutine;

    private bool isTyping = false;
    private bool isLineFinished = false;

    private GameObject player;

    void Start()
    {
        // Cari player berdasarkan tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Matikan movement player saat dialog dimulai
        SetPlayerMovement(false);

        story = new Story(inkJSON.text);
        dialoguePanel.SetActive(true);

        // if (useFadeTransition && blackScreen != null)
        // {
        //     StartCoroutine(FadeFromBlack());
        // }


        DisplayNextLine();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                SkipTyping();
            }
            else if (isLineFinished)
            {
                DisplayNextLine();
            }
        }
    }

    void DisplayNextLine()
    {
        if (story.canContinue)
        {
            string nextLine = story.Continue();

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeLine(nextLine));
        }
        else
        {
            SetPlayerMovement(true);

            if (npcMovement != null &&
                guideTargetSteps != null &&
                guideTargetSteps.Length > 0)
            {
                npcMovement.StartGuide(guideTargetSteps[0]);
            }
            else
            {
                Debug.LogWarning("guideTargetSteps kosong atau belum diisi!");
            }

            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        isTyping = true;
        isLineFinished = false;

        bool isAddingRichTextTag = false;

        foreach (char letter in line.ToCharArray())
        {
            if (!isTyping)
                yield break;

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                    isAddingRichTextTag = false;
            }
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        isTyping = false;
        isLineFinished = true;
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.maxVisibleCharacters = dialogueText.text.Length;

        isTyping = false;
        isLineFinished = true;
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(0.5f);

        if (!string.IsNullOrEmpty(nextSceneName) && Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueText.gameObject.SetActive(false);
            
            if (useFadeTransition && blackScreen != null)
            {
                StartCoroutine(FadeFromBlack());
            }

            Debug.LogWarning("Scene tidak ditemukan: " + nextSceneName);
        }
    }

    IEnumerator FadeAndLoadScene()
    {
        yield return StartCoroutine(FadeToBlack());

        if (!string.IsNullOrEmpty(nextSceneName) &&
            Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueText.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;

        blackScreen.gameObject.SetActive(true);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            blackScreen.alpha = Mathf.Lerp(
                0f,
                1f,
                elapsedTime / fadeDuration
            );

            yield return null;
        }

        blackScreen.alpha = 1f;
    }

    IEnumerator FadeFromBlack()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 1f;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            blackScreen.alpha = Mathf.Lerp(
                1f,
                0f,
                elapsedTime / fadeDuration
            );

            yield return null;
        }

        blackScreen.alpha = 0f;
    }

    void SetPlayerMovement(bool state)
    {
        if (player == null) return;

        // Nonaktifkan semua script movement di player
        MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = state;
            }
        }
    }
}
