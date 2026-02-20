using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Quest")]
    public int questID = -1;
    public int objectiveIndex = -1;
    public bool completeObjectiveAfterDialogue = false;

    private bool dialogueWasPlaying = false;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().DialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetDialoguePressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

        bool isPlaying = DialogueManager.GetInstance().DialogueIsPlaying;

        if (dialogueWasPlaying && !isPlaying)
        {
            // Dialogue baru saja selesai
            if (completeObjectiveAfterDialogue && questID != -1)
            {
                QuestManager.Instance.CompleteObjective(questID, objectiveIndex);
            }
        }

        dialogueWasPlaying = isPlaying;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
