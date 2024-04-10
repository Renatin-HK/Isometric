using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : HealthUI
{
    //REFERÊNCIA AO OBJETO QUE SERÁ ALTERADO NA UI
    [SerializeField]private Image playerHealthbarSprite;

    public override void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        // ALTERAÇÃO DE UI
         playerHealthbarSprite.fillAmount = (float)currentHealth / maxHealth;
    }
}
