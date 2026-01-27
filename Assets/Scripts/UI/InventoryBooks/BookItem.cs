using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemName", menuName = "BookPage/CreateSpices", order = 1)]

public class BookItem : ScriptableObject
{
    public string spiceName;
    public string description;
    public Sprite spiceImage;
}
