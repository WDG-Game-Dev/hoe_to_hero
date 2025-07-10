using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public float damageAmount = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player menyentuh spike!");

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount, transform.position);
                Debug.Log("Player terkena damage dari spike!");
            }
            else
            {
                Debug.LogWarning("Tidak ditemukan komponen PlayerHealth pada objek: " + other.name);
            }
        }
    }
}
