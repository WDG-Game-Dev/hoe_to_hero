using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject sceneTransitionTrigger;

    public void OpenDoorAndEnableSceneTrigger()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
            Debug.Log("Pintu dibuka!");
        }

        if (sceneTransitionTrigger != null)
        {
            sceneTransitionTrigger.SetActive(true);
            Debug.Log("Trigger pindah scene diaktifkan!");
        }
    }
}
