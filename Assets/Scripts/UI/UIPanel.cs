using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public GameObject panelToActivate;

    private void Start()
    {
        // Assuming your panel is initially disabled, you can activate it here.
        panelToActivate.SetActive(false);

        // Check if the condition is already met at the start
        CheckItemCollection();
    }

    public void CheckItemCollection()
    {
        // Check if the condition is met
        if (ItemDatabase.instance.itemListDestroy.Count == 3)
        {
            // Activate the panel
            panelToActivate.SetActive(true);

            // Optionally reset the item collection count if needed
            // ItemDatabase.instance.itemListDestroy.Clear();
        }
    }
}
