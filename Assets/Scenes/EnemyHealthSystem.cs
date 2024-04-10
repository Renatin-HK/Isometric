using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : BaseHealthSystem
{
    EnemyUIManager uIManager;
    void Start()
    {
        uIManager = FindObjectOfType<EnemyUIManager>();
        currentHealth = maxHealth;
    }

    public override void TakeDamage(int damageAmount)
    {
        // reduz a saúde do inimigo com base no dano recebido
        currentHealth -= damageAmount;

        // invoca o evento de dano
        InvokeDamageTaken(damageAmount);

        // atualiza a UI do inimigo pelo UIManager
        if (uIManager != null)
        {
            uIManager.UpdateEnemyHealthUI(currentHealth, maxHealth);
        }
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public override void Die()
    {
        // IMPLMENTAÇÃO DA MORTE DO INIMIGO
    }
}
