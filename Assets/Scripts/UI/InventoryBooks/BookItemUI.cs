using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookItemUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemDescription;

    public void UpdateUI(BookItem item)
    {
        itemImage.sprite = item.spiceImage;
        itemDescription.text = item.description;
    }
}
