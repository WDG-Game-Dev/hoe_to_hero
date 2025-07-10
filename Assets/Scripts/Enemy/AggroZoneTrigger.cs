using UnityEngine;
using System.Collections;

public class AgroZoneTrigger : MonoBehaviour
{
    [SerializeField] private GolemChase golem;
    [SerializeField] private GameObject bossHealthUI;
    [SerializeField] private AudioSource normalMusic;
    [SerializeField] private AudioSource bossMusic;
    [SerializeField] private string bossQuestText = "Defeat the Crystal Golem!";
    [SerializeField] private QuestUIManager questUI;

    private bool triggered = false;

    private void Start()
    {
        if (bossHealthUI != null)
            bossHealthUI.SetActive(false); 

        if (bossMusic != null)
        {
            bossMusic.Play();
            bossMusic.Pause();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;
        golem.StartChase(other.transform);
        StartCoroutine(HandleBossTrigger());
    }

    private IEnumerator HandleBossTrigger()
    {
        yield return new WaitForSeconds(0.2f);
        if (bossHealthUI != null)
            bossHealthUI.SetActive(true);
        if (questUI != null)
            questUI.SetQuest(bossQuestText);
        if (normalMusic != null && normalMusic.isPlaying)
            normalMusic.Stop();
        if (bossMusic != null)
            bossMusic.UnPause();
    }
    
    // ✅ FUNGSI BARU: Panggil ini saat bos sudah mati
    public void OnBossDefeated()
    {
        // 1. Matikan UI Darah Boss
        if (bossHealthUI != null)
        {
            bossHealthUI.SetActive(false);
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