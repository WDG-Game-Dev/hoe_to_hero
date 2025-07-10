using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Image healthBarFill;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;

    public float invulnerableDuration = 1.5f;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHealthUI();
    }

    public void TakeDamage(float amount, Vector2 attackerPosition)
    {
        if (isDead || isInvulnerable) return;

        currentHealth -= amount;
        Debug.Log("Player kena damage, sisa darah: " + currentHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Hurt();
            StartCoroutine(StartInvulnerability());
        }
    }

    private void Hurt()
    {
        if (animator != null)
        {
            animator.Play("Hurt");
        }

        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
            Invoke(nameof(EnableMovement), 0.5f); // durasi animasi hurt
        }
    }

    private void EnableMovement()
    {
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = true;
        }
    }

    private System.Collections.IEnumerator StartInvulnerability()
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(0.3f);

        float blinkTime = 0.15f;
        float timer = 0f;

        while (timer < invulnerableDuration)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkTime);
            timer += blinkTime;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        isInvulnerable = false;
    }

    private void Die()
    {
        isDead = true;

        if (animator != null)
            animator.Play("Death");

        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        GetComponent<Collider2D>().enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        Invoke(nameof(ShowGameOverUI), 2f); // tampilkan UI game over setelah 2 detik
    }

    private void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    private void ShowGameOverUI()
    {
        var ui = FindFirstObjectByType<GameOverUIManager>();
        if (ui != null)
        {
            ui.ShowGameOver();
        }
    }
}
