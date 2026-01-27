using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    private AudioManager audioManager;  // Reference to the AudioManager

    Vector2 movement;

    private bool isWalking = false;  // Flag to track whether the player is walking

    public VectorValue startingPosition;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();  // Find the AudioManager in the scene
    }

    private void Awake()
    {
        if (ItemDatabase.instance != null)
        {
            if (ItemDatabase.instance.playerPosition != null && SceneManager.GetActiveScene().name == "MapMain")
            {
                transform.position = ItemDatabase.instance.playerPosition;
            }
        }
    }

    public void SetMovement(Vector2 inputMovement)
    {
        movement = inputMovement;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (inputMovement.x == 1 || inputMovement.x == -1 || inputMovement.y == 1 || inputMovement.y == -1)
        {
            animator.SetFloat("LastMoveHorizontal", inputMovement.x);
            animator.SetFloat("LastMoveVertical", inputMovement.y);
        }
    }

    void Update()
    {
        // Check if the DialogueManager instance exists before using it
        DialogueManager dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager != null && dialogueManager.DialogueIsPlaying)
        {
            return;
        }

        // Check if the player is walking based on input from InputManager
        if (InputManager.GetInstance().IsPlayerWalking())
        {
            if (!isWalking)
            {
                isWalking = true;
                audioManager.PlaySfx(audioManager.walking);
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                // Stop the footstep sound when the movement keys are released
                audioManager.sfxSource.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        // Move the player only if walking
        if (isWalking)
        {
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
        }
    }
}