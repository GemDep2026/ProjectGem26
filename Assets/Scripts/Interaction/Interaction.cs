using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public string puzzleScene;

    public int itemId;
    public int questID;
    public int objectiveIndex;

    private Vector2 playerPos;

    [SerializeField] protected GameObject interactUI;
    public bool AllowInteract { get { return allowInteract; } }

    private bool allowInteract = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerPos = collision.transform.position;

            interactUI.SetActive(true);

            allowInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactUI.SetActive(false);
            allowInteract = false;
        }
    }

    private void Update()
    {
        if (allowInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {             
                OnInteract();
            }
        }
    }

    public virtual void OnInteract()
    {
        interactUI.SetActive(false);
        QuestManager.Instance.CompleteObjective(questID, objectiveIndex);
        ItemDatabase.instance.SavePlayerPosition(playerPos);
        ItemDatabase.instance.AddItemToList(itemId);
        if (!string.IsNullOrEmpty(puzzleScene))
        {
            SceneManager.LoadScene(puzzleScene);
        }
    }

    private void Start()
    {
        if (ItemDatabase.instance.itemListDestroy.Contains(itemId))
        {
            Destroy(gameObject);
        }
    }
}
