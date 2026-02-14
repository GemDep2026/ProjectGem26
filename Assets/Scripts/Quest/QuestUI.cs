using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questText;

    void Update()
    {
        Quest current = QuestManager.Instance.GetCurrentQuest();

        string text = current.questName + "\n";

        foreach(var obj in current.objectives)
        {
            if(obj.isCompleted)
                text += "☑ " + obj.objectiveName + "\n";
            else
                text += "☐ " + obj.objectiveName + "\n";
        }

        questText.text = text;
    }
}
