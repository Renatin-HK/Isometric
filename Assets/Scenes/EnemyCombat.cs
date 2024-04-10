using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // Referência para o jogador
    private Transform player;
    [SerializeField][Range(1, 100)]private int damageAmount;
    [SerializeField]private float rotationSpeed = 3f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        AimAtPlayer(); // Realizar mira no jogador
        Attack(); // Realizar ataque se estiver pronto
    }

    // Método para realizar a mira no jogador
private void AimAtPlayer()
{
    // Direção para onde o inimigo deve mirar
    Vector3 targetDirection = player.position - transform.position;
    targetDirection.y = 0; // Garantir que a mira seja apenas no plano horizontal

    // Rotação suave do inimigo na direção do jogador
    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
}

    // Método para realizar o ataque
private void Attack()
{
    // Aqui você pode implementar uma condição para verificar se o inimigo está pronto para atacar, por exemplo, ao terminar de mirar no jogador.
    // Por enquanto, vamos supor que o inimigo ataca sempre que estiver mirando no jogador.

    // Chama o método DealDamage de TakeDamage para causar dano ao jogador
    PlayerHealthSystem takeDamage = GetComponent<PlayerHealthSystem>();
    if (takeDamage != null)
    {
        takeDamage.TakeDamage(damageAmount);
    }
}
}
