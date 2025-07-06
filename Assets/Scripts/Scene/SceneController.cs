using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Buat Load banyak scene kode dibawah tapi yang lama ga dihapus
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("NextScene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    IEnumerator LoadSceneWithTransition(string sceneName)
    {
        transitionAnim.SetTrigger("NextScene");
        yield return new WaitForSeconds(1f);  // Tunggu animasi fade out selesai
        SceneManager.LoadSceneAsync(sceneName);
        // Setelah scene baru load, animasi fade in bisa dipanggil di Start scene baru
    }
}
