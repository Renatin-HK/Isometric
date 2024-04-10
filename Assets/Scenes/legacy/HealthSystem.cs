using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Range(1, 200)]public int maxHealth = 100; // Saúde máxima do jogador
    private int currentHealth; // Saúde atual do jogador

    public delegate void OnHealthChanged(int currentHealth, int maxHealth);
    public event OnHealthChanged healthChangedEvent; // Evento chamado quando a saúde do jogador muda

    void Start()
    {
        currentHealth = maxHealth; // Inicializa a saúde atual com a saúde máxima
    }

    // Método para causar dano ao jogador
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduz a saúde atual pelo valor do dano

        // Garante que a saúde atual não ultrapasse a saúde máxima
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Dispara o evento de mudança de saúde
        if (healthChangedEvent != null)
        {
            healthChangedEvent(currentHealth, maxHealth);
        }

        // Verifica se o jogador está sem saúde e chama o método de derrota
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método chamado quando o jogador morre
    void Die()
    {
        // Aqui você pode adicionar lógica adicional, como mostrar uma animação de morte, reiniciar o nível, etc.
        Debug.Log("Player is dead!");
    }
}
