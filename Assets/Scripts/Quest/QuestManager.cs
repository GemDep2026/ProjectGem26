using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> quests = new List<Quest>();

    private int currentQuestIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        StartFirstQuest();
    }

    void StartFirstQuest()
    {
        
    }

    public void CompleteObjective(int questID, int objectiveIndex)
    {
        Quest quest = quests[currentQuestIndex];

        if (quest.questID != questID) return;

        quest.objectives[objectiveIndex].isCompleted = true;

        if (quest.IsCompleted())
        {
            Debug.Log("Quest Completed!");

            currentQuestIndex++;

            if (currentQuestIndex < quests.Count)
            {
                Debug.Log("Next Quest Activated");
            }
            else
            {
                Debug.Log("All Quest Finished!");
                SceneManager.LoadScene("Scene2");
            }
        }
    }
    public Quest GetCurrentQuest()
    {
        if (currentQuestIndex >= quests.Count)
        return null;

        return quests[currentQuestIndex];
    }
}
