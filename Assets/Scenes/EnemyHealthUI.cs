using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : HealthUI
{
    [SerializeField] private Image enemyHealthbarSprite;

    public override void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        // Atualiza a barra de vida do inimigo
        enemyHealthbarSprite.fillAmount = (float)currentHealth / maxHealth;

    }
}
