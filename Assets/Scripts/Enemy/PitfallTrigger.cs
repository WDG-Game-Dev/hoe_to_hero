using UnityEngine;

public class PitfallTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player jatuh ke jurang!");

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                // Pastikan langsung mematikan player (pakai damage besar)
                health.TakeDamage(9999f, transform.position);
            }
        }
    }
}
