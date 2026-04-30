using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]


public class SimpleDialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Guide Dialogues")]
    public TextAsset[] guideDialogues;

    [Header("Scene After Dialogue")]
    public string[] sceneAfterDialogue;

    [Header("Activate Objects After Dialogue")]
    public List<GameObjectGroup> activateAfterDialogue;

    [Header("Deactivate Objects After Dialogue")]
    public List<GameObjectGroup> deactivateAfterDialogue;

    [Header("Activate Objects Before Dialogue")]
    public List<GameObjectGroup> activateBeforeDialogue;

    [Header("Deactivate Objects Before Dialogue")]
    public List<GameObjectGroup> deactivateBeforeDialogue;

    private bool playerInRange;
    private bool dialogueWasPlaying = false;
    private int dialogueIndex = 0;

    void Awake()
    {
        visualCue.SetActive(false);
    }

    void Update()
    {
        DialogueManager dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager == null) return;

        if (playerInRange && !dialogueManager.DialogueIsPlaying)
        {
            visualCue.SetActive(true);

            if (InputManager.GetInstance().GetDialoguePressed())
            {
                int currentIndex = Mathf.Clamp(dialogueIndex, 0, guideDialogues.Length - 1);

                // ACTIVATE BEFORE
                if (activateBeforeDialogue.Count > currentIndex)
                {
                    foreach (GameObject obj in activateBeforeDialogue[currentIndex].objects)
                    {
                        if (obj != null)
                        {
                            Debug.Log("Activate BEFORE: " + obj.name);
                            obj.SetActive(true);
                        }
                    }
                }

                // DEACTIVATE BEFORE
                if (deactivateBeforeDialogue.Count > currentIndex)
                {
                    foreach (GameObject obj in deactivateBeforeDialogue[currentIndex].objects)
                    {
                        if (obj != null)
                            obj.SetActive(false);
                    }
                }

                // START DIALOGUE
                if (guideDialogues.Length > 0)
                {
                    dialogueManager.EnterDialogueMode(guideDialogues[currentIndex]);
                }
                else
                {
                    dialogueManager.EnterDialogueMode(inkJSON);
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

        bool isPlaying = dialogueManager.DialogueIsPlaying;

        if (dialogueWasPlaying && !isPlaying)
        {
            // ACTIVATE AFTER DIALOGUE
            if (activateAfterDialogue.Count > dialogueIndex)
            {
                foreach (GameObject obj in activateAfterDialogue[dialogueIndex].objects)
                {
                    if (obj != null)
                        obj.SetActive(true);
                }
            }

            // DEACTIVATE AFTER DIALOGUE
            if (deactivateAfterDialogue.Count > dialogueIndex)
            {
                foreach (GameObject obj in deactivateAfterDialogue[dialogueIndex].objects)
                {
                    if (obj != null)
                        obj.SetActive(false);
                }
            }
            // SCENE CHANGE
            if (sceneAfterDialogue.Length > dialogueIndex)
            {
                string sceneName = sceneAfterDialogue[dialogueIndex];

                if (!string.IsNullOrEmpty(sceneName))
                {
                    SceneManager.LoadScene(sceneName);
                    return;
                }
            }


            dialogueIndex++;

            if (dialogueIndex >= guideDialogues.Length)
            {
                dialogueIndex = 0; // loop ulang
            }
        }

        dialogueWasPlaying = isPlaying;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }
}