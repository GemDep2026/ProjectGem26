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

    void Start()
    {
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
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextSceneName);
    }
}
