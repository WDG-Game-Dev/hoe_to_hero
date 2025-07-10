using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Opsional: Cek tag agar hanya player yang bisa menghancurkan
        if (other.CompareTag("Player"))
        {
            Debug.Log("Objek disentuh oleh Player, menghancurkan...");
            Destroy(gameObject);
        }
    }
}
