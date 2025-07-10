using System.Collections; // Diperlukan untuk Coroutine
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene to Load")]
    public string sceneNameToLoad = "HouseScene"; // Nama scene bisa diubah dari Inspector

    [Header("Fade Settings")]
    public CanvasGroup fadeCanvasGroup; // Hubungkan FadePanel ke sini
    public float fadeDuration = 1.5f;   // Durasi transisi fade dalam detik

    private bool isFading = false; // Mencegah klik ganda

    // Fungsi ini dipanggil oleh tombol Play
    public void PlayGame()
    {
        // Hanya jalankan jika tidak sedang dalam proses fading
        if (isFading)
        {
            return;
        }
        
        StartCoroutine(FadeAndLoadScene());
    }

    // Fungsi ini dipanggil oleh tombol Quit
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    // Coroutine untuk menjalankan animasi fade out lalu memuat scene baru
    private IEnumerator FadeAndLoadScene()
    {
        isFading = true;

        // Blokir interaksi dengan tombol selama proses fade
        fadeCanvasGroup.blocksRaycasts = true;

        float timer = 0f;

        // Loop untuk mengubah alpha dari 0 ke 1 (transparan ke hitam)
        while (timer < fadeDuration)
        {
            // Menghitung nilai alpha berdasarkan waktu
            fadeCanvasGroup.alpha = timer / fadeDuration;
            
            // Tambahkan waktu yang telah berlalu
            timer += Time.deltaTime;

            // Tunggu frame berikutnya sebelum melanjutkan loop
            yield return null;
        }

        // Pastikan alpha di akhir adalah 1
        fadeCanvasGroup.alpha = 1f;

        // Setelah fade selesai, muat scene baru
        SceneManager.LoadScene(sceneNameToLoad);
    }
}