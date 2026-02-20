using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questText;

    void Update()
    {
        if (QuestManager.Instance == null) return;

        Quest current = QuestManager.Instance.GetCurrentQuest();

        if (current == null)
        {
            questText.text = "All Quests Completed!";
            return;
        }

        string text = current.questName + "\n";

        foreach(var obj in current.objectives)
        {
            if(obj.isCompleted)
                text += "[v] " + obj.objectiveName + "\n";
            else
                text += "[ ] " + obj.objectiveName + "\n";
        }

        questText.text = text;
    }
}

