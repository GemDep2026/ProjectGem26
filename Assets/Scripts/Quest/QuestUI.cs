using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questTitle;

    public Transform objectiveContainer;
    public ObjectiveItemUI objectivePrefab;

    private List<ObjectiveItemUI> objectiveItems = new();

    void Update()
    {
        if (QuestManager.Instance == null) return;

        Quest current = QuestManager.Instance.GetCurrentQuest();

        if (current == null)
        {
            questTitle.text = "All Quests Completed!";
            return;
        }

        questTitle.text = current.questName;

        RefreshObjectives(current);
    }

    void RefreshObjectives(Quest quest)
    {
        // Tambah UI jika kurang
        while (objectiveItems.Count < quest.objectives.Count)
        {
            ObjectiveItemUI item =
                Instantiate(objectivePrefab, objectiveContainer);

            objectiveItems.Add(item);
        }

        // Update semua objective
        for (int i = 0; i < objectiveItems.Count; i++)
        {
            if (i < quest.objectives.Count)
            {
                objectiveItems[i].gameObject.SetActive(true);
                objectiveItems[i].Setup(quest.objectives[i]);
            }
            else
            {
                objectiveItems[i].gameObject.SetActive(false);
            }
        }
    }
}