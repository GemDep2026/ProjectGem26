using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveItemUI : MonoBehaviour
{
    public Image icon;
    public Sprite completedSprite;
    public Sprite incompleteSprite;

    public TextMeshProUGUI objectiveText;

    public void Setup(QuestObjectiveData objective)
    {
        objectiveText.text = objective.objectiveName;
        icon.sprite = objective.isCompleted
            ? completedSprite
            : incompleteSprite;
    }
}