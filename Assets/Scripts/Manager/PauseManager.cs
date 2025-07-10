using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Hubungkan Panel Pause Menu dari Inspector
    public GameObject pauseMenuPanel;

    // Variabel untuk mengecek apakah game sedang di-pause
    private bool isPaused = false;

    void Start()
    {
        // Pastikan menu pause tidak aktif saat game dimulai
        pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        // Cek jika tombol Escape ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // Jika sedang pause, panggil fungsi Resume
                ResumeGame();
            }
            else
            {
                // Jika tidak sedang pause, panggil fungsi Pause
                PauseGame();
            }
        }
    }

    // Fungsi untuk mem-pause game
    public void PauseGame()
    {
        isPaused = true;
        // Mengaktifkan panel menu pause
        pauseMenuPanel.SetActive(true);
        // Menghentikan waktu di dalam game
        Time.timeScale = 0f;
    }

    // Fungsi untuk melanjutkan game (dipanggil oleh tombol Continue)
    public void ResumeGame()
    {
        isPaused = false;
        // Menonaktifkan panel menu pause
        pauseMenuPanel.SetActive(false);
        // Mengembalikan waktu ke kecepatan normal
        Time.timeScale = 1f;
    }

    // Fungsi untuk kembali ke Main Menu (dipanggil oleh tombol Quit)
    public void QuitToMainMenu()
    {
        // PENTING: Kembalikan timeScale ke 1 sebelum pindah scene
        // agar scene berikutnya tidak ikut ter-pause.
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Ganti "MainMenu" jika nama scene Anda berbeda
    }
}