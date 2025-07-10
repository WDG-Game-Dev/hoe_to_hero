using UnityEngine;

public class GolemChase : MonoBehaviour
{
    public float attackDamage = 15f;
    public float specialAttackDamage = 30f;
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    [Range(0f, 1f)] public float specialAttackChance = 0.3f; // 30% chance special

    private Rigidbody2D rb;
    private Animator animator;
    private Transform target;
    private bool chasingPlayer = false;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private Collider2D attackZoneCollider;
    private bool isHalfHealth = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Cari child "AttackZone"
        Transform attackZone = transform.Find("AttackZone");
        if (attackZone != null)
        {
            attackZoneCollider = attackZone.GetComponent<Collider2D>();
            if (attackZoneCollider != null)
            {
                attackZoneCollider.enabled = false;
            }
        }
        else
        {
            Debug.LogWarning("AttackZone tidak ditemukan!");
        }
    }

    void Update()
    {
        if (!chasingPlayer || target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        // Hadap ke player
        transform.localScale = new Vector3(
            target.position.x > transform.position.x ? -1 : 1,
            1,
            1
        );

        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                if (isHalfHealth && Random.value < specialAttackChance)
                    SpecialAttack();
                else
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

    private void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;

        rb.linearVelocity = Vector2.zero;
        animator.Play("AttackGolem");

        if (attackZoneCollider != null)
        {
            Invoke(nameof(EnableAttackZone), 0.4f);
            Invoke(nameof(DisableAttackZone), 0.6f);
        }

        Invoke(nameof(FinishAttack), 1.2f);
    }

    private void SpecialAttack()
    {
        if (isAttacking) return;
        isAttacking = true;

        rb.linearVelocity = Vector2.zero;
        animator.Play("SpecialAttackGolem");

        attackDamage = specialAttackDamage;

        if (attackZoneCollider != null)
        {
            Invoke(nameof(EnableAttackZone), 0.5f);
            Invoke(nameof(DisableAttackZone), 0.8f);
        }

        Invoke(nameof(ResetAttackDamage), 1.5f);
        Invoke(nameof(FinishAttack), 3f);
    }

    private void EnableAttackZone() => attackZoneCollider.enabled = true;
    private void DisableAttackZone() => attackZoneCollider.enabled = false;
    private void ResetAttackDamage() => attackDamage = 15f;
    private void FinishAttack() => isAttacking = false;

    public void StartChase(Transform player)
    {
        chasingPlayer = true;
        target = player;
        attackTimer = attackCooldown;

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

        if (animator != null && animator.isActiveAndEnabled)
        {
            animator.Play("IdleGolem");
        }
    }

    public void EnableSpecialAttackMode()
    {
        isHalfHealth = true;
        Debug.Log("Boss masuk Phase 2!");
    }
}
