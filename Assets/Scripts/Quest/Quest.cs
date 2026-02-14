using System.Collections.Generic;

[System.Serializable]
public class Quest
{
    public int questID;
    public string questName;

    public List<QuestObjectiveData> objectives = new List<QuestObjectiveData>();

    public bool IsCompleted()
    {
        foreach (var obj in objectives)
        {
            if (!obj.isCompleted)
                return false;
        }
        return true;
    }
}

[System.Serializable]
public class QuestObjectiveData
{
    public string objectiveName;
    public bool isCompleted;
}

public enum QuestStatus
{
    Locked,
    Active,
    Completed
}

