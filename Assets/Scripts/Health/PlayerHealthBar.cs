using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    public void SetHealth(float currentHealth, float maxHealth)
    {
        float fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
        fillImage.fillAmount = fillAmount;
    }
}
