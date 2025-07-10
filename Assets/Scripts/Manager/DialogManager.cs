using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public string[] dialogLines;

    private int currentLineIndex;
    private bool isDialogActive = false;
    private System.Action onDialogComplete;
    private Player1Movement playerMovement;
    private HeroKnight heroKnight;

    private void Start()
    {
        dialogPanel.SetActive(false);
        playerMovement = FindFirstObjectByType<Player1Movement>();
    }

    private void Update()
    {
        if (isDialogActive && Input.GetKeyDown(KeyCode.F))
        {
            AdvanceDialog();
        }
    }

    public void StartDialog(string[] lines, System.Action callback = null)
    {
        dialogLines = lines;
        currentLineIndex = 0;
        isDialogActive = true;
        dialogPanel.SetActive(true);
        dialogText.text = dialogLines[currentLineIndex];

        onDialogComplete = callback; // ✅ Simpan callback-nya

        UIManager.Instance.HideAllGameplayUI();

        if (playerMovement != null)
            playerMovement.enabled = false;
        if (heroKnight != null)
            heroKnight.enabled = false; // Nonaktifkan movement
    }

    private void AdvanceDialog()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLineIndex];
        }
        else
        {
            EndDialog();
        }
    }

    private void EndDialog()
    {
        isDialogActive = false;
        dialogPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;

        UIManager.Instance.ShowQuestUI(true);

        // ✅ Jalankan callback jika ada
        if (onDialogComplete != null)
        {
            onDialogComplete.Invoke();
            onDialogComplete = null; // Bersihkan callback setelah dipanggil
        }
    }
}
