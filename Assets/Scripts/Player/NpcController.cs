using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private Animator animator;
    private Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    private Vector2 lastMoveDirection = Vector2.down; // Default direction

    void Start()
    {
        animator = GetComponent<Animator>();
        FindPatrolPoints();
    }

    void Update()
    {
        if (!DialogueManager.GetInstance().DialogueIsPlaying)
        {
            Patrol();
            UpdateAnimator();
        }
        else
        {
            // If a dialogue is playing, set the NPC to idle
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
    }

    void FindPatrolPoints()
    {
        // Find all GameObjects with the "PatrolPoint" tag
        GameObject[] patrolPointObjects = GameObject.FindGameObjectsWithTag("PatrolPoint");

        // Initialize the array with the length of the found objects
        patrolPoints = new Transform[patrolPointObjects.Length];

        // Assign the positions of the found objects to the patrolPoints array
        for (int i = 0; i < patrolPointObjects.Length; i++)
        {
            patrolPoints[i] = patrolPointObjects[i].transform;
        }
    }

    void Patrol()
    {
        Vector2 targetPosition = patrolPoints[currentPatrolIndex].position;

        // move to the next patrol point
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;
        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);

        // Store the last movement direction
        lastMoveDirection = moveDirection;
    }

    void UpdateAnimator()
    {
        animator.SetFloat("Horizontal", lastMoveDirection.x);
        animator.SetFloat("Vertical", lastMoveDirection.y);
    }
}