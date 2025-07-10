using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    public GameObject questUI;
    public TMP_Text questText;

    // Tambahkan UI lainnya jika ada, misalnya:
    public GameObject healthUI;
    public GameObject potionUI;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // agar tetap ada saat pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowQuestUI(bool show)
    {
        if (questUI != null)
            questUI.SetActive(show);
    }

    public void HideAllGameplayUI()
    {
        if (questUI != null) questUI.SetActive(false);
        if (healthUI != null) healthUI.SetActive(false);
        if (potionUI != null) potionUI.SetActive(false);
    }

    public void ShowAllGameplayUI()
    {
        if (questUI != null) questUI.SetActive(true);
        if (healthUI != null) healthUI.SetActive(true);
        if (potionUI != null) potionUI.SetActive(true);
    }

    public void UpdateQuest(string newText)
    {
        if (questText != null)
            questText.text = newText;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        // Re-link jika perlu
        if (questUI == null)
            questUI = GameObject.Find("QuestUI");

        if (questText == null && questUI != null)
            questText = questUI.GetComponentInChildren<TMPro.TMP_Text>();

        if (healthUI == null)
            healthUI = GameObject.Find("HealthUI");

        if (potionUI == null)
            potionUI = GameObject.Find("PotionUI");
    }
}
