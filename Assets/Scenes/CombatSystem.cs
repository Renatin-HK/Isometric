using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    // Método para causar dano a um objeto com HealthSystem
    public void DealDamage(GameObject target, int damageAmount)
    {
        HealthSystem healthSystem = target.GetComponent<HealthSystem>(); // Obtém o componente HealthSystem do objeto alvo

        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damageAmount); // Chama o método TakeDamage do HealthSystem para causar dano ao alvo
        }
    }
}
