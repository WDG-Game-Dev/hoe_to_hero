using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFadeManager : MonoBehaviour
{
    public Image fadeImage; // Drag UI Image hitam fullscreen
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true); // pastikan aktif

            var color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;

            Debug.Log("[Fade] Awake: Fade image disiapkan dengan alpha 0");
        }
        else
        {
            Debug.LogWarning("[Fade] Fade Image belum di-assign di inspector!");
        }
    }

    public IEnumerator FadeOut()
    {
        Debug.Log("[Fade] Mulai FadeOut...");

        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        Debug.Log("[Fade] Selesai FadeOut (layar jadi hitam)");
    }

    public IEnumerator FadeIn()
    {
        Debug.Log("[Fade] Mulai FadeIn...");

        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;

        Debug.Log("[Fade] Selesai FadeIn (layar kembali terang)");
    }
}
