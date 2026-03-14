using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int[] objectiveIndex;
    public bool completeObjectiveAfterDialogue = false;

    [Header("Guide NPC")]
    public bool startGuideAfterDialogue = false;
    public NpcMovement npcMovement;
    // public PlayerController player;

    [Header("Guide Step")]
    public bool nextGuideAfterDialogue = false;

    [Header("Scene After Dialogue")]
    public string[] sceneAfterDialogue;

    private bool dialogueWasPlaying = false;
    private int dialogueIndex = 0;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }
    void Update()
    {
        DialogueManager dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager == null) return;

        if (playerInRange && !dialogueManager.DialogueIsPlaying)
        {
            if (npcMovement != null && npcMovement.IsGuiding())
            {
                visualCue.SetActive(false);
                return;
            }

            visualCue.SetActive(true);

            if (InputManager.GetInstance().GetDialoguePressed())
            {
                if (npcMovement != null && npcMovement.IsGuiding())
                    return;

                if (npcMovement != null && guideDialogues.Length > 0)
                {
                    dialogueIndex = Mathf.Clamp(dialogueIndex, 0, guideDialogues.Length - 1);
                    dialogueManager.EnterDialogueMode(guideDialogues[dialogueIndex]);
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
            if (completeObjectiveAfterDialogue && questID != -1)
            {
                if (objectiveIndex.Length > dialogueIndex)
                {
                    QuestManager.Instance.CompleteObjective(questID, objectiveIndex[dialogueIndex]);
                }
            }

            if (npcMovement != null && guideTargetSteps.Length > dialogueIndex)
            {
                npcMovement.StartGuide(guideTargetSteps[dialogueIndex]);
            }

            if (sceneAfterDialogue.Length > dialogueIndex)
            {
                string sceneName = sceneAfterDialogue[dialogueIndex];

                if (!string.IsNullOrEmpty(sceneName))
                {
                    SceneManager.LoadScene(sceneName);
                    return;
                }
            }

            dialogueIndex = Mathf.Min(dialogueIndex + 1, guideTargetSteps.Length - 1);
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
