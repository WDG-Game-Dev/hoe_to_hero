using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            GolemChase boss = GetComponentInParent<GolemChase>();
            float damage = boss != null ? boss.attackDamage : 15f;

            player.TakeDamage(damage, transform.position); // Kirim posisi golem
            Debug.Log("Player kena serangan boss");
        }

        // Hanya kena 1x
        GetComponent<Collider2D>().enabled = false;
    }
}
}
