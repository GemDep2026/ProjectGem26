using UnityEngine;
using UnityEngine.UI;

public class UISessions : MonoBehaviour
{
    public GameObject uiPanel;

    private static bool panelDisplayed = false;

    void Start()
    {
        if (!panelDisplayed)
        {
            uiPanel.SetActive(true);
            panelDisplayed = true;
        }
        else
        {
            uiPanel.SetActive(false);
        }
    }

    public void CloseUIPanel()
    {
        uiPanel.SetActive(false);
    }
}