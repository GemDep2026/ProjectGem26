using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<int> itemListDestroy;
    public List<BookItem> inventory;

    public Vector2 playerPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemToList(int id)
    {
        itemListDestroy.Add(id);
       
    }

    public void AddItem(BookItem itemToAdd)
    {
        inventory.Add(itemToAdd);
        BookInventoryUI.Instance.AddItemUI(itemToAdd);
    }

    public void SavePlayerPosition(Vector2 pos)
    {
        playerPosition = pos;
    }
}
