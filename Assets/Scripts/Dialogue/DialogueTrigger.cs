using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Guide Dialogues")]
    public TextAsset[] guideDialogues;

    [Header("Guide Target Step")]
    public int[] guideTargetSteps;

    [Header("Quest")]
    public int questID = -1;
    public int objectiveIndex = -1;
    public bool completeObjectiveAfterDialogue = false;

    [Header("Guide NPC")]
    public bool startGuideAfterDialogue = false;
    public NpcMovement npcMovement;
    // public PlayerController player;

    [Header("Guide Step")]
    public bool nextGuideAfterDialogue = false;

    private bool dialogueWasPlaying = false;
    private int dialogueIndex = 0;

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
                if (npcMovement != null && guideDialogues.Length > 0)
                {
                    dialogueIndex = Mathf.Clamp(dialogueIndex, 0, guideDialogues.Length - 1);
                    DialogueManager.GetInstance().EnterDialogueMode(guideDialogues[dialogueIndex]);
                }
                else
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

        bool isPlaying = DialogueManager.GetInstance().DialogueIsPlaying;

        if (dialogueWasPlaying && !isPlaying)
        {
            // Complete Quest
            if (completeObjectiveAfterDialogue && questID != -1)
            {
                QuestManager.Instance.CompleteObjective(questID, objectiveIndex);
            }

            if (startGuideAfterDialogue && npcMovement != null)
            {
                npcMovement.StartGuide(1);
            }

            // Guide berikutnya
            if (nextGuideAfterDialogue && npcMovement != null)
            {
                npcMovement.NextGuide();
            }

            if (npcMovement != null && guideTargetSteps.Length > dialogueIndex)
            {
                npcMovement.StartGuide(guideTargetSteps[dialogueIndex]);
            }

            dialogueIndex++;
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
