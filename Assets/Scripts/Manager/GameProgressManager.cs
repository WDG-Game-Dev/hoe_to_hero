using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    // ===== FLAGS UNTUK DIALOG / QUEST =====
    public bool hasTriggeredKey1 = false;
    public bool hasTriggeredKey2 = false;
    public bool hasTriggeredKey3 = false;

    public bool hasOpenedDoorDialog = false;

    // Fungsi bantu: cek apakah semua kunci sudah didapat
    public bool AllKeysCollected()
    {
        return hasTriggeredKey1 && hasTriggeredKey2 && hasTriggeredKey3;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // agar bertahan antar scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
