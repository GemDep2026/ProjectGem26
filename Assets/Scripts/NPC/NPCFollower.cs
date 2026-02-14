using UnityEngine;

public class NPCFollower : MonoBehaviour
{
    private PlayerController player;

    public int followIndex = 10; // semakin besar semakin jauh mengikuti
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 lastDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        if (PlayerController.positionHistory.Count > followIndex)
        {
            Vector3 targetPosition = PlayerController.positionHistory[followIndex];
            float distance = Vector2.Distance(rb.position, targetPosition);

            if (player.IsWalking && distance > 0.1f)
            {
                Vector2 direction = (targetPosition - transform.position).normalized;

                lastDirection = direction; // simpan arah terakhir

                rb.MovePosition(Vector2.MoveTowards(
                    rb.position,
                    targetPosition,
                    moveSpeed * Time.fixedDeltaTime
                ));

                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
                animator.SetFloat("Speed", 1f);
            }
            else
            {
                // IDLE
                animator.SetFloat("Speed", 0f);
                animator.SetFloat("Horizontal", lastDirection.x);
                animator.SetFloat("Vertical", lastDirection.y);
            }
        }
    }



}
