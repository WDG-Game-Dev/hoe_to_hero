using UnityEngine;

public class GolemPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float patrolDuration = 3f;

    private float patrolTimer = 0f;
    private bool movingRight = true;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            patrolTimer = 0f;
            movingRight = !movingRight;
        }

        Vector2 velocity = new Vector2(movingRight ? moveSpeed : -moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = velocity;

        // Flip sprite sesuai arah
        transform.localScale = new Vector3(movingRight ? -1 : 1, 1, 1);

        // Mainkan animasi jalan
        if (animator != null)
            animator.Play("MoveGolem");
    }
}
