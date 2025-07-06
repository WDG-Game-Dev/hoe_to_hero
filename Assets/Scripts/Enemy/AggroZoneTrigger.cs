using UnityEngine;

public class AgroZoneTrigger : MonoBehaviour
{
    [SerializeField] private GolemChase golem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            golem.StartChase(other.transform);
            Debug.Log("Player masuk aggro zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            golem.StopChase();
        }
    }
}
