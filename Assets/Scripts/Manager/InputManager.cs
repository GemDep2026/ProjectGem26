using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerController playerController;
    public PauseManager pauseManager;
    public BookController bookController;
    public Interaction interaction;
    public bool dialoguePressed = false;
    public bool submitPressed = false;

    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public void Update()
    {
        HandlePlayerInput();
        HandlePauseInput();
        HandleBookInput();
        HandleDialogueInput();
        HandleSubmitInput();
    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    public bool IsPlayerWalking()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        return horizontal != 0f || vertical != 0f;
    }

    public void HandlePlayerInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        playerController.SetMovement(new Vector2(horizontal, vertical));
    }

    public void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseManager.TogglePause();
        }
    }

    public void HandleBookInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bookController.ToggleBook();
        }
    }

    public void HandleDialogueInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            dialoguePressed = true;
        }
        else
        {
            dialoguePressed = false;
        }
    }

    public void HandleSubmitInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            submitPressed = true;
        }
        else
        {
            submitPressed = false;
        }
    }

    public bool GetDialoguePressed()
    {
        bool result = dialoguePressed;
        dialoguePressed = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }
}
