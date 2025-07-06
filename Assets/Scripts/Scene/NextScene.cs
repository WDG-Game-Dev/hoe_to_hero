using UnityEngine;

public class NextScene : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.instance.NextLevel();
        }
    }
}