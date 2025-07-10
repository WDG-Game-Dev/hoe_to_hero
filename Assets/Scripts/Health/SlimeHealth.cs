using UnityEngine;
using System.Collections;

public class SlimeHealth : MonoBehaviour
{
    public float maxHealth = 30f;
    private float currentHealth;

    private Animator animator;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Slime kena damage! Sisa darah: " + currentHealth);

        StartCoroutine(BlinkEffect()); // Efek blink

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.1f);
    }

    private IEnumerator BlinkEffect()
    {
        float blinkDuration = 0.1f;
        int blinkCount = 4;

        for (int i = 0; i < blinkCount; i++)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkDuration);

            if (spriteRenderer != null)
                spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
