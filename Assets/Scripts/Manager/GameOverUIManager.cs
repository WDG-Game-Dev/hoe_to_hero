using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameOverUIManager : MonoBehaviour
{
    public CanvasGroup gameOverPanel;  // ini satu panel, semua isi termasuk tombol & bg
    public AudioSource sfxPlayer;

    private void Start()
    {
        HideGameOver();
    }

    public void ShowGameOver()
    {
        // mainkan suara
        if (sfxPlayer != null)
            sfxPlayer.Play();

        // mulai fade-in panel
        StartCoroutine(FadeInPanel());
    }

    private IEnumerator FadeInPanel()
    {
        float duration = 2f;
        float t = 0f;
        gameOverPanel.alpha = 0;
        gameOverPanel.interactable = false;
        gameOverPanel.blocksRaycasts = false;

        while (t < duration)
        {
            // GANTI baris ini
            t += Time.unscaledDeltaTime; // Gunakan unscaledDeltaTime

            gameOverPanel.alpha = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }

        // Bagian ini sekarang akan tercapai bahkan jika Time.timeScale = 0
        gameOverPanel.alpha = 1;
        gameOverPanel.interactable = true;
        gameOverPanel.blocksRaycasts = true;
    }

    public void HideGameOver()
    {
        gameOverPanel.alpha = 0;
        gameOverPanel.interactable = false;
        gameOverPanel.blocksRaycasts = false;
    }

    public void RetryScene()
    {
        Debug.Log("Retry clicked!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
{
    Debug.Log("Return to Menu clicked!");
#if UNITY_EDITOR
    SceneManager.LoadScene("MainMenu");
#else
    EditorApplication.isPlaying = false;
#endif
}
}
