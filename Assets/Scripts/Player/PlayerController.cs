using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    public static List<Vector3> positionHistory = new List<Vector3>();
    public int maxHistory = 100;

    private AudioManager audioManager;  // Reference to the AudioManager

    Vector2 movement;

    private bool isWalking = false;  // Flag to track whether the player is walking

    public VectorValue startingPosition;

    public bool IsWalking => isWalking;

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
        if (Mathf.Abs(inputMovement.x) > Mathf.Abs(inputMovement.y))
        {
            movement = new Vector2(Mathf.Sign(inputMovement.x), 0);
        }
        else if (Mathf.Abs(inputMovement.y) > 0)
        {
            movement = new Vector2(0, Mathf.Sign(inputMovement.y));
        }
        else
        {
            movement = Vector2.zero;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement != Vector2.zero)
        {
            animator.SetFloat("LastMoveHorizontal", movement.x);
            animator.SetFloat("LastMoveVertical", movement.y);
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

    void LateUpdate()
    {
        positionHistory.Insert(0, transform.position);

        if (positionHistory.Count > maxHistory)
            positionHistory.RemoveAt(positionHistory.Count - 1);
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