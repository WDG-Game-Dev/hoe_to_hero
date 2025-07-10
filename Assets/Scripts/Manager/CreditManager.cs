using UnityEngine;
using TMPro; // Penting untuk menggunakan TextMeshPro
using System.Collections;
using UnityEngine.SceneManagement; // Jika ingin pindah scene di akhir

public class CreditController : MonoBehaviour
{
    [Header("Referensi UI")]
    [SerializeField] private TMP_Text creditTextUI; // Drag TextMeshPro UI Anda ke sini

    [Header("Konten Credit")]
    [Tooltip("Setiap elemen adalah satu baris teks yang akan muncul bergantian.")]
    [TextArea(2, 5)]
    [SerializeField] private string[] creditLines;

    [Header("Pengaturan Waktu")]
    [SerializeField] private float fadeDuration = 1.5f; // Durasi untuk fade in dan fade out
    [SerializeField] private float displayDuration = 3f;  // Durasi teks tampil di layar

    [Header("Aksi Setelah Selesai")]
    [SerializeField] private string sceneToLoadAfter = "MainMenu"; // Scene yang akan dimuat setelah credit selesai
    
    void Start()
    {
        // Pastikan teks tidak terlihat di awal
        creditTextUI.color = new Color(creditTextUI.color.r, creditTextUI.color.g, creditTextUI.color.b, 0);

        // Mulai Coroutine untuk menampilkan credit
        StartCoroutine(AnimateCredits());
    }

    private IEnumerator AnimateCredits()
    {
        // Tunggu sejenak sebelum credit pertama muncul
        yield return new WaitForSeconds(1.5f);

        // Loop melalui setiap baris teks credit
        for (int i = 0; i < creditLines.Length; i++)
        {
            // Atur teksnya
            creditTextUI.text = creditLines[i];

            // --- FADE IN ---
            yield return FadeText(1f, fadeDuration); // Munculkan teks

            // --- TAHAN TAMPIL ---
            yield return new WaitForSeconds(displayDuration); // Tahan selama beberapa detik

            // --- FADE OUT ---
            yield return FadeText(0f, fadeDuration); // Hilangkan teks

            // Beri jeda singkat sebelum teks berikutnya muncul
            yield return new WaitForSeconds(0.5f);
        }

        // Setelah semua credit selesai, lakukan sesuatu
        Debug.Log("Credit selesai!");
        if (!string.IsNullOrEmpty(sceneToLoadAfter))
        {
            SceneManager.LoadScene(sceneToLoadAfter);
        }
    }

    // Coroutine khusus untuk menangani proses fade
    private IEnumerator FadeText(float targetAlpha, float duration)
    {
        float timer = 0f;
        Color currentColor = creditTextUI.color;
        float startAlpha = currentColor.a;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            creditTextUI.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }

        // Pastikan alpha di akhir sesuai target
        creditTextUI.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }
}