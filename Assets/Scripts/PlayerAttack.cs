using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Cooldown")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attack2Cooldown = 1f;
    [SerializeField] private float attack3Cooldown = 2f;

    [Header("Damage Settings")]
    [SerializeField] private float attackDamage = 10f;

    [Header("Attack Hitbox")]
    [SerializeField] private Collider2D attackHitbox;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private float cooldownTimer2 = Mathf.Infinity;
    private float cooldownTimer3 = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        if (attackHitbox != null)
            attackHitbox.enabled = false; // Pastikan mati dulu
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        if (Input.GetMouseButton(1) && cooldownTimer2 > attack2Cooldown && playerMovement.canAttack())
            Attack2();

        if (Input.GetKey(KeyCode.LeftControl) && cooldownTimer3 > attack3Cooldown && playerMovement.canAttack())
            Attack3();

        cooldownTimer += Time.deltaTime;
        cooldownTimer2 += Time.deltaTime;
        cooldownTimer3 += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }

    private void Attack2()
    {
        anim.SetTrigger("attack2");
        cooldownTimer2 = 0;
    }

    private void Attack3()
    {
        anim.SetTrigger("attack3");
        cooldownTimer3 = 0;
    }

    // Animation Events — panggil dari animasi
    public void EnableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!attackHitbox.enabled) return;

        if (other.CompareTag("Boss"))
        {
            BossHealth boss = other.GetComponent<BossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(attackDamage);
                Debug.Log("Boss terkena pukulan player: -" + attackDamage);
                attackHitbox.enabled = false; // agar 1x hit saja
            }
        }
        if (other.CompareTag("Slime"))
        {
            SlimeHealth slime = other.GetComponent<SlimeHealth>();
            if (slime != null)
            {
                slime.TakeDamage(attackDamage);
                Debug.Log("Slime terkena pukulan player: -" + attackDamage);
                attackHitbox.enabled = false;
            }
        }
    }
}
