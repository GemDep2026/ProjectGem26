using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    // ─── State ───────────────────────────────────────────────
    private enum NPCState { Patrol, Guide, IdleAtTarget }
    private NPCState currentState = NPCState.Patrol;

    // ─── Gerakan ──────────────────────────────────────────────
    public float moveSpeed = 2.0f;
    private Vector2 lastMoveDirection = Vector2.down;

    // ─── Patrol ───────────────────────────────────────────────
    [Header("Patrol Points")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    // ─── Guide ────────────────────────────────────────────────
    [Header("Guide Points")]
    public Transform[] guidePoints;
    private int guideStep = 0;
    private int targetGuideStep = 0;

    [SerializeField] private bool startGuideOnStart = false;
    [SerializeField] private int startingGuideStep = 0;

    [Header("Guide Follow Settings")]
    public Transform player;
    public float waitDistance = 3f;

    // ─── Komponen ─────────────────────────────────────────────
    private Animator animator;

    // ══════════════════════════════════════════════════════════

    void Start()
    {
        animator = GetComponent<Animator>();

        guideStep = 0;
        targetGuideStep = startingGuideStep;

        if (startGuideOnStart)
            currentState = NPCState.Guide;
    }

    void Update()
    {
        // Pause saat dialog sedang berjalan
        if (DialogueManager.GetInstance() != null &&
            DialogueManager.GetInstance().DialogueIsPlaying)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        switch (currentState)
        {
            case NPCState.Patrol:       Patrol();                               break;
            case NPCState.Guide:        GuideMove();                            break;
            case NPCState.IdleAtTarget: animator.SetBool("isWalking", false);   break;
        }

        UpdateAnimator();
    }

    // ══════════════════════════════════════════════════════════
    // PATROL
    // ══════════════════════════════════════════════════════════

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        MoveToTarget(patrolPoints[currentPatrolIndex].position);

        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    // ══════════════════════════════════════════════════════════
    // GUIDE
    // ══════════════════════════════════════════════════════════

    void GuideMove()
    {
        if (guidePoints == null || guidePoints.Length == 0) return;

        // Tunggu player kalau terlalu jauh
        if (Vector2.Distance(transform.position, player.position) > waitDistance)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        MoveToTarget(guidePoints[guideStep].position);

        if (Vector2.Distance(transform.position, guidePoints[guideStep].position) < 0.1f)
        {
            if (guideStep < targetGuideStep)
                guideStep++;
            else
            {
                currentState = NPCState.IdleAtTarget;
                animator.SetBool("isWalking", false);
            }
        }
    }

    // ══════════════════════════════════════════════════════════
    // MOVEMENT & ANIMATION
    // ══════════════════════════════════════════════════════════

    void MoveToTarget(Vector2 targetPos)
    {
        Vector2 currentPos = transform.position;
        float xDiff = targetPos.x - currentPos.x;
        float yDiff = targetPos.y - currentPos.y;

        Vector2 moveDirection;

        // Prioritas: selesaikan sumbu X dulu, baru Y
        if      (Mathf.Abs(xDiff) > 0.05f) moveDirection = new Vector2(Mathf.Sign(xDiff), 0);
        else if (Mathf.Abs(yDiff) > 0.05f) moveDirection = new Vector2(0, Mathf.Sign(yDiff));
        else
        {
            animator.SetBool("isWalking", false);
            return;
        }

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
        lastMoveDirection = moveDirection;

        animator.SetFloat("Horizontal",       moveDirection.x);
        animator.SetFloat("Vertical",         moveDirection.y);
        animator.SetFloat("LastMoveHorizontal", moveDirection.x);
        animator.SetFloat("LastMoveVertical",   moveDirection.y);
        animator.SetBool("isWalking", true);
    }

    void UpdateAnimator()
    {
        animator.SetFloat("Horizontal", lastMoveDirection.x);
        animator.SetFloat("Vertical",   lastMoveDirection.y);
    }

    // ══════════════════════════════════════════════════════════
    // PUBLIC API
    // ══════════════════════════════════════════════════════════

    /// <summary>Minta NPC mengantar player hingga guide step tertentu.</summary>
    public void StartGuide(int step)
    {
        targetGuideStep = Mathf.Clamp(step, 0, guidePoints.Length - 1);
        if (targetGuideStep <= guideStep) return;
        currentState = NPCState.Guide;
    }

    /// <summary>Kembali ke mode patrol.</summary>
    public void ResumePatrol() => currentState = NPCState.Patrol;

    /// <summary>Paksa lanjut ke guide step berikutnya.</summary>
    public void NextGuide()
    {
        guideStep++;
        if (guideStep < guidePoints.Length)
            currentState = NPCState.Guide;
    }

    public int  GetCurrentGuideStep() => guideStep;
    public bool IsGuiding()           => currentState == NPCState.Guide;
    public bool IsMoving()            => currentState == NPCState.Guide || currentState == NPCState.Patrol;
}
