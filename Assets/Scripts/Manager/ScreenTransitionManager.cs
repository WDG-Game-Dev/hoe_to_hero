using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadePanel; // Drag UI Image hitam ke sini
    public float fadeDuration = 1.5f;

    // Fungsi ini yang akan kita panggil dari DialogTrigger
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // Pastikan panel terlihat tapi transparan di awal
        fadePanel.gameObject.SetActive(true);
        Color panelColor = fadePanel.color;
        panelColor.a = 0;
        fadePanel.color = panelColor;
        
        float timer = 0f;

        // Proses fade-out (alpha dari 0 ke 1)
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            panelColor.a = alpha;
            fadePanel.color = panelColor;
            yield return null;
        }

        // Setelah layar benar-benar hitam, pindah scene
        SceneManager.LoadScene(sceneName);
    }
}