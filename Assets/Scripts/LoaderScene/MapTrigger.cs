using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTrigger : MonoBehaviour
{
    [Header("Scene")]
    public string mapName;
    public Vector2 playerPosition;
    public string itemComplete;

    [Header("Quest")]
    public bool completeQuestOnTrigger = false;
    public int questID;
    public int objectiveIndex;

    [Header("Fade")]
    public bool useFadeTransition = true;

    private bool alreadyTriggered = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            alreadyTriggered = true;

            // COMPLETE QUEST
            if (completeQuestOnTrigger)
            {
                QuestManager.Instance.CompleteObjective(questID, objectiveIndex);
            }


            if (useFadeTransition && FadeManager.Instance != null)
            {
                FadeManager.Instance.FadeToScene(mapName);
            }
            else
            {
                SceneManager.LoadScene(mapName);
            }
        }
    }
}