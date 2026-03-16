using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class Interactable : MonoBehaviour
{
    public GameObject interactUI;
    public GameObject pressFText;

    private bool playerNear = false; // deklarasi variabel

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            pressFText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            pressFText.SetActive(false);
        }
    }
}
