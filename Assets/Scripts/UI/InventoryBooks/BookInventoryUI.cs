using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookInventoryUI : MonoBehaviour
{
    public static BookInventoryUI Instance;
    [SerializeField] BookItemUI itemUIPrefab;
    [SerializeField] private GameObject SpicesContent;

    public void AddItemUI(BookItem item)
    {
        BookItemUI itemUI = Instantiate(itemUIPrefab, SpicesContent.transform);
        itemUI.UpdateUI(item);
    }

    private void Awake()
    {
        Instance = this;
    }
}
