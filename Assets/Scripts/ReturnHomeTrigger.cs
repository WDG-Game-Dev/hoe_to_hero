using UnityEngine;
using Cinemachine;

public class ReturnHomeTrigger : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject healthUI;
    public DialogTrigger dialogTrigger;
    public ScreenFadeManager screenFade;
    public CinemachineVirtualCamera virtualCam; 

    private bool hasTriggered = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;
        StartCoroutine(HandleTransition());
    }

    private System.Collections.IEnumerator HandleTransition()
    {
        if (screenFade != null)
            yield return StartCoroutine(screenFade.FadeOut());

        // Ganti karakter setelah fade hitam
        player2.SetActive(false);
        player1.SetActive(true);

        if (healthUI != null)
            healthUI.SetActive(true);

        if (screenFade != null)
            yield return StartCoroutine(screenFade.FadeIn());

        // Trigger dialog
        if (dialogTrigger != null)
            dialogTrigger.TriggerDialog();

        if (virtualCam != null)
            virtualCam.Follow = player1.transform;

        // Nonaktifkan trigger agar tidak dipicu lagi
        gameObject.SetActive(false);
    }
}
