using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;

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
            // Dialog selesai → player boleh bergerak lagi
            SetPlayerMovement(true);

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
        dialoguePanel.SetActive(false);
        dialogueText.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Scene tidak ditemukan: " + nextSceneName);
        }
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
