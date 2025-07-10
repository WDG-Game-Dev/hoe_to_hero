using UnityEngine;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText;

    public void SetQuest(string quest)
    {
        if (questText != null)
            questText.text = quest;
    }
}
