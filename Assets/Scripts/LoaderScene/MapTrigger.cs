using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTrigger : MonoBehaviour
{
    public string mapName;
    public Vector2 playerPosition;
    public string itemComplete;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (SceneManager.GetActiveScene().name == "MapMain")
            {
                ItemDatabase.instance.SavePlayerPosition(playerPosition);

                // Check if the item list count is at least 3 before loading the scene
                if (ItemDatabase.instance.itemListDestroy.Count == 3)
                {
                    // Use the serialized itemComplete if provided, otherwise use the default mapName
                    string sceneToLoad = string.IsNullOrEmpty(itemComplete) ? mapName : itemComplete;
                    SceneManager.LoadScene(sceneToLoad);
                    return; // Don't proceed to the next scene load
                }
            }

            SceneManager.LoadScene(mapName);
        }
    }
}
