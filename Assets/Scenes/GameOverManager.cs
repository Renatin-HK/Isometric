using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public HealthSystem playerHealth; // Referência ao componente HealthSystem do jogador

    void Start()
    {
        // Inscreve-se no evento de mudança de saúde do jogador
        playerHealth.healthChangedEvent += CheckGameOver;
    }

    // Método chamado sempre que a saúde do jogador muda
    void CheckGameOver(int currentHealth, int maxHealth)
    {
        // Verifica se a saúde do jogador chegou a zero
        if (currentHealth <= 0)
        {
            GameOver(); // Chama o método de derrota
        }
    }

    // Método chamado quando o jogador perde
    void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
