using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string nextSceneName;  // Ini yang nanti muncul di Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneSpawnManager.lastScene = SceneManager.GetActiveScene().name;
            SceneController.instance.LoadScene(nextSceneName);
        }
    }
}
