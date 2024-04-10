using UnityEngine;
public class UIManager : MonoBehaviour
{
    public HealthUI playerHealthUI;

    // Método para atualizar a UI do jogador
    public void UpdatePlayerHealthUI(int currentHealth, int maxHealth)
    {
        playerHealthUI.UpdateHealthUI(currentHealth, maxHealth);
    }

}
