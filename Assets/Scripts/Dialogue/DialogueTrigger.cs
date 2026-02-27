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

    [Header("Guide NPC")]
    public bool startGuideAfterDialogue = false;
    public NpcMovement npcMovement;
    // public PlayerController player;

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
            // Complete Quest
            if (completeObjectiveAfterDialogue && questID != -1)
            {
                QuestManager.Instance.CompleteObjective(questID, objectiveIndex);
            }

            // Start Guide
            if (startGuideAfterDialogue && npcMovement != null)
            {
                npcMovement.StartGuide();

                // if (player != null)
                //     player.FollowNPC(npcMovement.transform);
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
