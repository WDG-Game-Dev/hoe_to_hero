using UnityEngine;

public class GolemChase : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;

    private Rigidbody2D rb;
    private Animator animator;

    private Transform target;
    private bool chasingPlayer = false;
    private float attackTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!chasingPlayer || target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        // Hadap ke player
        if (target.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        if (isAttacking)
        {
            // Jangan lakukan apa-apa saat sedang menyerang
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                Attack();
                attackTimer = 0f;
            }
            else
            {
                animator.Play("IdleGolem");
            }
        }
        else
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
            animator.Play("MoveGolem");
        }
    }


    private bool isAttacking = false;
    private void Attack()
    {
        if (isAttacking) return; // cegah spam
        isAttacking = true;

        rb.linearVelocity = Vector2.zero;
        animator.Play("AttackGolem");
        Debug.Log("Golem menyerang!");

        // Reset setelah selesai (misalnya 1.2 detik)
        Invoke(nameof(FinishAttack), 2.5f);


        if (animator != null && !animator.isActiveAndEnabled)
        {
            Debug.LogWarning("Animator tidak aktif saat akan memainkan animasi.");
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;
    }


    public void StartChase(Transform player)
    {
        chasingPlayer = true;
        target = player;
        attackTimer = attackCooldown; // bisa serang langsung
        var patrol = GetComponent<GolemPatrol>();
        if (patrol != null)
        {
            patrol.enabled = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void StopChase()
    {
        chasingPlayer = false;
        rb.linearVelocity = Vector2.zero;

        if (animator != null)
        {
            if (animator.isActiveAndEnabled)
            {
                animator.Play("IdleGolem");
            }
            else
            {
                Debug.LogWarning("Animator tidak aktif, skip pemanggilan animasi.");
            }
        }
    }

}
