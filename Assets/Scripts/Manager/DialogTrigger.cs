using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    public string[] dialogLines;
    public UnityEvent onDialogFinished;
    public DialogConditionType conditionType = DialogConditionType.None;
    public KeyProgressFlag progressFlagToSet = KeyProgressFlag.None;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;
        if (!CheckConditionMet()) return;

        if (other.CompareTag("Player"))
        {
            DialogManager dialog = FindFirstObjectByType<DialogManager>();
            if (dialog != null)
            {
                dialog.StartDialog(dialogLines, () =>
                {
                    onDialogFinished.Invoke();
                });

                hasTriggered = true;

                SetProgressFlag();
            }

            Debug.Log("Player menyentuh trigger!");
        }

        if (hasTriggered || !CheckConditionMet()) return;

        // Tambahan khusus untuk FoundDoor
        if (progressFlagToSet == KeyProgressFlag.FoundDoor && GameProgressManager.Instance.hasOpenedDoorDialog)
            return;
    }

    public void TriggerDialog()
    {
        if (hasTriggered) return;
        if (!CheckConditionMet()) return;

        DialogManager dialog = FindFirstObjectByType<DialogManager>();
        if (dialog != null)
        {
            dialog.StartDialog(dialogLines, () =>
            {
                onDialogFinished.Invoke();
            });

            hasTriggered = true;

            SetProgressFlag();
        }
    }

    private void SetProgressFlag()
    {
        if (GameProgressManager.Instance == null) return;

        switch (progressFlagToSet)
        {
            case KeyProgressFlag.Key1:
                GameProgressManager.Instance.hasTriggeredKey1 = true;
                break;
            case KeyProgressFlag.Key2:
                GameProgressManager.Instance.hasTriggeredKey2 = true;
                break;
            case KeyProgressFlag.Key3:
                GameProgressManager.Instance.hasTriggeredKey3 = true;
                break;
            case KeyProgressFlag.FoundDoor:
                GameProgressManager.Instance.hasOpenedDoorDialog = true;
                break;
        }
    }

    private bool CheckConditionMet()
    {
        switch (conditionType)
        {
            case DialogConditionType.RequireAllKeys:
                return GameProgressManager.Instance != null &&
                       GameProgressManager.Instance.AllKeysCollected();

            case DialogConditionType.None:
            default:
                return true;
        }
    }

    public enum DialogConditionType
    {
        None,
        RequireAllKeys
    }

    public enum KeyProgressFlag
    {
        None,
        Key1,
        Key2,
        Key3,
        FoundDoor
    }
}
