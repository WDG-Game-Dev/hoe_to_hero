using UnityEngine;

public class PlayerHealthTest : MonoBehaviour
{
    public PlayerHealthBar healthBar;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= 10f;
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }
}
