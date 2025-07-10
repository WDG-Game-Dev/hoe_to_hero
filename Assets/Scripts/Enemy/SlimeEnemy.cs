using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private int moveDirection = 1; // 1 = kanan, -1 = kiri

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        // Periksa ujung platform dengan raycast arah ke bawah dari posisi maju ke depan slime
        Vector2 checkPos = groundCheck.position + Vector3.right * moveDirection * 0.3f; // Tambah offset ke depan
        RaycastHit2D groundInfo = Physics2D.Raycast(checkPos, Vector2.down, groundCheckDistance, groundLayer);

        if (!groundInfo.collider)
        {
            Flip();
        }
    }

    void Flip()
    {
        moveDirection *= -1;
        spriteRenderer.flipX = moveDirection < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                Vector2 hitFrom = transform.position;
                player.TakeDamage(10, hitFrom);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Vector2 checkPos = groundCheck.position + Vector3.right * moveDirection * 0.3f;
            Gizmos.DrawLine(checkPos, checkPos + Vector2.down * groundCheckDistance);
        }
    }
}
