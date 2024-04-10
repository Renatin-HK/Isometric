using System;
using UnityEngine;

public class PlayerHealthSystem : BaseHealthSystem
{
    // Adicione campos ou propriedades específicas do jogador, se necessário

    private UIManager uIManager; //referência ao controlador da UI

    void Start()
    {
        currentHealth = maxHealth;
        uIManager = FindObjectOfType<UIManager>();
    }

    // private void FixedUpdate()
    // {
    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         TakeDamage(10);
    //     }
    // }


    // Implementação do método para receber dano
    public override void TakeDamage(int damageAmount)
    {
        // Reduz a saúde do jogador com base no dano recebido
        currentHealth -= damageAmount;

        // Chama o evento de dano
        InvokeDamageTaken(damageAmount);

        // Atualiza a UI do jogador através do UIManager
        if (uIManager != null)
        {
            uIManager.UpdatePlayerHealthUI(currentHealth, maxHealth);
        }

        // Verifica se o jogador está morto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Implementação do método para tratar a morte do jogador
    public override void Die()
    {
        // Lógica para quando o jogador morre
    }

    internal void TakeDamage(object damageAmount)
    {
        throw new NotImplementedException();
    }
}
