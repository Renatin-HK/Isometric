using UnityEngine;

public class EnemyDistanceAttack : MonoBehaviour
{
    public Transform player; // Referência ao transform do jogador
    public float moveSpeed = 3f; // Velocidade de movimento dos inimigos
    public float retreatDistance = 5f; // Distância de recuo dos inimigos
    public float rotationSpeed = 5f; // Velocidade de rotação dos inimigos

    private void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();
        }
    }

    // Move o inimigo em direção ao jogador
    private void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    // Rotaciona o inimigo na direção do jogador
    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Recua o inimigo para manter uma distância segura
    private void Retreat()
    {
        Vector3 direction = transform.position - player.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);
    }
}
