using UnityEngine;
public abstract class BaseHealthSystem : MonoBehaviour
{
    [SerializeField]protected int currentHealth;
    [SerializeField]protected int maxHealth;

    // Delegado para lidar com eventos de dano
    public delegate void DamageEventHandler(int damageAmount);

    // Evento que será invocado quando o inimigo receber dano
    public event DamageEventHandler OnDamageTaken;

    // Método abstrato para receber dano
    public abstract void TakeDamage(int damageAmount);

    // Método abstrato para tratar a morte
    public abstract void Die();

    // Método para chamar o evento de dano
    protected virtual void InvokeDamageTaken(int damageAmount)
    {
        // Verifica se há assinantes do evento antes de invocá-lo
        OnDamageTaken?.Invoke(damageAmount);
    }
}
