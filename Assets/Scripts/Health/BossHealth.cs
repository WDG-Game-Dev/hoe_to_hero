using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{ 
    // ✅ BARU: Tambahkan referensi ke AgroZoneTrigger
    [SerializeField] private AgroZoneTrigger agroZone;
    [SerializeField] private QuestUIManager questUI;

    public float maxHealth = 500f;
    private float currentHealth;
    public Image healthFill;
    private Animator animator;
    private bool isDead = false;
    private bool specialAttackTriggered = false;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        Debug.Log("Boss terkena serangan. Sisa darah: " + currentHealth);
        UpdateHealthUI();

        if (!specialAttackTriggered && currentHealth <= maxHealth / 2f)
        {
            specialAttackTriggered = true;
            GolemChase chase = GetComponent<GolemChase>();
            if (chase != null)
            {
                chase.EnableSpecialAttackMode();
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        isDead = true;

        // ✅ BARU: Panggil fungsi di AgroZoneTrigger untuk mematikan UI
        if (agroZone != null)
        {
            agroZone.OnBossDefeated();
        }

        if (animator != null)
        {
            int deathHash = Animator.StringToHash("DeathGolem");
            if (animator.HasState(0, deathHash))
                animator.Play(deathHash);
            else
                Debug.LogWarning("Animator state 'DeathGolem' tidak ditemukan!");
        }

        GolemChase chase = GetComponent<GolemChase>();
        if (chase != null)
            chase.enabled = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.bodyType = RigidbodyType2D.Static;

        if (this.questUI != null)
        {
            this.questUI.SetQuest("Explore what lies beyond the Crystal Golem");
        }
    }
}