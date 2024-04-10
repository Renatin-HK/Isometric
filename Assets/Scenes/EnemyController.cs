using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemyBullet
{
    public GameObject projectilePrefab; // Prefab do objeto que será disparado
    public Transform spawnPoint; // Ponto de origem do disparo
    public int projectileCount = 1; // Quantidade de projéteis a serem instanciados
    [SerializeField][Range(1, 50)]public int shootForce = 20; // Força do disparo
    [Range(0f, 1f)] public float accuracy = 1f; // Precisão do disparo (0: aleatório, 1: direto)
    [SerializeField][Range(0, 5)] public float attackCooldown = 2f;

}

public class EnemyController : MonoBehaviour
{
    public EnemyBullet bulletConfig;
    public enum EnemyState
    {
        Patrolling,
        Chasing,
        Attacking,
        Fleeing
    }

    public EnemyState currentState = EnemyState.Patrolling;

    private float rotationSpeed = 20f;
    [SerializeField] bool canShoot = true;

    private Transform player;
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionDistance = 5f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float fleeDistance = 10f;

    private int currentWaypointIndex = 0;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                PatrollingUpdate();
                break;
            case EnemyState.Chasing:
                ChasingUpdate();
                break;
            case EnemyState.Attacking:
                LookAtDirection(player.position);
                AttackingUpdate();
                break;
            case EnemyState.Fleeing:
                FleeingUpdate();
                break;
        }
    }

    private void PatrollingUpdate()
    {
        MoveTowards(waypoints[currentWaypointIndex].position);
        LookAtDirection(waypoints[currentWaypointIndex].position);

        if (PlayerIsNear())
        {
            currentState = EnemyState.Chasing;
        }
        else if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

    }

    private void ChasingUpdate()
    {
        MoveTowards(player.position);
        LookAtDirection(player.position);

        if (Vector3.Distance(transform.position, player.position) <= attackDistance &&
            Vector3.Distance(transform.position, player.position) > fleeDistance)
        {
            currentState = EnemyState.Attacking;
        }
        else if (Vector3.Distance(transform.position, player.position) > detectionDistance)
        {
            currentState = EnemyState.Patrolling;
        }
    }

    private void AttackingUpdate()
    {
        // Disparar o projétil
        if (bulletConfig.projectilePrefab != null && canShoot)
        {
            LookAtDirection(player.position);
            Shoot();
            canShoot = false;
            StartCoroutine(AttackCooldown());

        }


        if (Vector3.Distance(transform.position, player.position) < fleeDistance)
        {
            currentState = EnemyState.Fleeing;
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            currentState = EnemyState.Chasing;
        }
    }

    private void FleeingUpdate()
    {
        Vector3 fleeDirection = transform.position - player.position;
        Vector3 fleePosition = transform.position + fleeDirection.normalized * fleeDistance;
        LookAtDirection(fleePosition);
        MoveTowards(fleePosition);

        if (Vector3.Distance(transform.position, player.position) > fleeDistance)
        {
            currentState = EnemyState.Attacking;
        }
    }

    private bool PlayerIsNear()
    {
        return Vector3.Distance(transform.position, player.position) <= detectionDistance;
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void LookAtDirection(Vector3 lookDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(bulletConfig.attackCooldown);
        canShoot = true;
    }

    private void Shoot()
    {
        // Calcula um desvio aleatório baseado na precisão
        Vector3 deviation = Random.insideUnitCircle * (1f - bulletConfig.accuracy);

        // Instancia o objeto projetil a partir do prefab no ponto de origem com um desvio aleatório
        GameObject projectile = Instantiate(bulletConfig.projectilePrefab, bulletConfig.spawnPoint.position + deviation, bulletConfig.spawnPoint.rotation);

        // Obtém o componente Rigidbody do projetil
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Aplica a força para disparar o objeto
        rb.AddForce((bulletConfig.spawnPoint.forward + deviation) * bulletConfig.shootForce, ForceMode.Impulse);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fleeDistance);
    }

}
