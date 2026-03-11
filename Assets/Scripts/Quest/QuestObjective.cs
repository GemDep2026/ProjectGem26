using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public int questID;
    public int objectiveIndex;

    public void Interact()
    {
        QuestManager.Instance.CompleteObjective(questID, objectiveIndex);
        gameObject.SetActive(false); // item hilang
    }
}
