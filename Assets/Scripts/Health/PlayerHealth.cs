//using Unity.VisualScripting;
//using UnityEngine;

//public class PlayerHealth : MonoBehaviour
//{
//    public int health;
//    public GameObject[] healthUI;

//    void TakeDamage()
//    {
//        health--;
//        if (health <= 0)
//        {
//            health = 0;
//            print("print dead");
//        }
//        healthUI[health].SetActive(false);
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//       if(collision.CompareTag("Enemy"))
//        {
//            TakeDamage();
//        }
//    }
//}


using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;  // Misalnya, pemain memiliki 3 poin kesehatan
    public GameObject[] healthUI;  // UI untuk kesehatan pemain

    private bool isInvulnerable = false;  // Untuk menghindari kerusakan ganda dalam waktu singkat

    void TakeDamage()
    {
        if (isInvulnerable) return;  // Cegah kerusakan ganda jika pemain sedang dalam kondisi tidak rentan

        health--;
        if (health <= 0)
        {
            health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Matikan healthUI berdasarkan nilai health
        if (health >= 0 && health < healthUI.Length)  // Memastikan indeks valid
        {
            healthUI[health].SetActive(false);  // Menyembunyikan UI kesehatan
        }

        isInvulnerable = true;  // Mengaktifkan mode tidak rentan
        Invoke("ResetInvulnerability", 1f);  // Mengatur kembali rentan setelah 1 detik (atau waktu yang sesuai)
    }

    // Mengatur ulang status invulnerable
    void ResetInvulnerability()
    {
        isInvulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object Hit"))
        {
            TakeDamage();  // Pemain terkena damage dari musuh
        }

        if (collision.CompareTag("DeadZone"))
        {
            health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart scene
        }
    }
}
