using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    private enum NPCState
    {
        Patrol,
        Guide,
        IdleAtTarget
    }

    private NPCState currentState = NPCState.Patrol;

    public float moveSpeed = 2.0f;
    [Header("Patrol Points")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    [Header("Guide Points")]
    public Transform[] guidePoints;
    private int currentGuideIndex = 0;

    private Animator animator;
    private Vector2 lastMoveDirection = Vector2.down; // Default direction

        void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogueManager.GetInstance() != null &&
            DialogueManager.GetInstance().DialogueIsPlaying)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        switch (currentState)
        {
            case NPCState.Patrol:
                Patrol();
                break;

            case NPCState.Guide:
                GuideMove();
                break;

            case NPCState.IdleAtTarget:
                animator.SetBool("isWalking", false);
                break;
        }

        UpdateAnimator();
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        MoveToTarget(patrolPoints[currentPatrolIndex].position);

        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void GuideMove()
    {
        if (guidePoints == null || guidePoints.Length == 0)
            return;

        MoveToTarget(guidePoints[currentGuideIndex].position);

        if (Vector2.Distance(transform.position, guidePoints[currentGuideIndex].position) < 0.1f)
        {
            currentGuideIndex++;

            if (currentGuideIndex >= guidePoints.Length)
            {
                currentState = NPCState.IdleAtTarget;
                animator.SetBool("isWalking", false);
            }
        }
    }

    void MoveToTarget(Vector2 targetPos)
    {
        Vector2 currentPos = transform.position;

        float xDiff = targetPos.x - currentPos.x;
        float yDiff = targetPos.y - currentPos.y;

        Vector2 moveDirection = Vector2.zero;

        // Prioritas selesaikan X dulu
        if (Mathf.Abs(xDiff) > 0.05f)
        {
            moveDirection = new Vector2(Mathf.Sign(xDiff), 0);
        }
        else if (Mathf.Abs(yDiff) > 0.05f)
        {
            moveDirection = new Vector2(0, Mathf.Sign(yDiff));
        }
        else
        {
            animator.SetBool("isWalking", false);
            return;
        }

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);

        lastMoveDirection = moveDirection;

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("LastMoveHorizontal", moveDirection.x);
        animator.SetFloat("LastMoveVertical", moveDirection.y);
        animator.SetBool("isWalking", true);
    }

    void UpdateAnimator()
    {
        animator.SetFloat("Horizontal", lastMoveDirection.x);
        animator.SetFloat("Vertical", lastMoveDirection.y);
    }

    public void StartGuide()
    {
        currentGuideIndex = 0;
        currentState = NPCState.Guide;
    }

    public void ResumePatrol()
    {
        currentState = NPCState.Patrol;
    }


}