using UnityEngine;

public class ArrowSelector : MonoBehaviour
{
    public RectTransform arrow;
    public float offsetX = -40f;

    private void Start()
    {
        arrow.gameObject.SetActive(false);
    }

    public void ShowArrow(RectTransform target)
    {
        arrow.position = target.position + Vector3.right * offsetX;
        arrow.gameObject.SetActive(true);
    }

    public void HideArrow()
    {
        arrow.gameObject.SetActive(false);
    }
}