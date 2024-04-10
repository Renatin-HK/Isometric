using System.Collections;
using UnityEngine;

[System.Serializable]
public class ShootConfig
{
    public GameObject projectilePrefab; // Prefab do objeto que será disparado
    public Transform spawnPoint; // Ponto de origem do disparo
    public int projectileCount = 1; // Quantidade de projéteis a serem instanciados
    public float shootForce = 20f; // Força do disparo
    [Range(0f, 1f)] public float accuracy = 1f; // Precisão do disparo (0: aleatório, 1: direto)
}

public class PlayerShoot : MonoBehaviour
{
    public ShootConfig[] shootConfigs; // Lista de configurações de disparo
    public float cooldownDuration = 1f; // Duração do tempo de recarga em segundos
    public LayerMask groundLayer; // Camada do terreno para o raycast

    private bool canShoot = true; // Flag para verificar se o personagem pode disparar

    void Update()
    {
        // Verifica se o personagem pode disparar e se o botão esquerdo do mouse foi pressionado
        if (canShoot && Input.GetMouseButtonDown(0))
        {
            // Lance um raycast a partir da posição do mouse na tela na direção da câmera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // Dispara o objeto de acordo com a configuração e a posição de mira no chão
                foreach (var config in shootConfigs)
                {
                    Shoot(config,new Vector3 (hit.point.x, hit.point.y + config.spawnPoint.position.y, hit.point.z)); // Dispara o objeto de acordo com a configuração e a posição de mira no chão
                }
            }
        }
    }


void Shoot(ShootConfig config, Vector3 targetPosition)
{
    for (int i = 0; i < config.projectileCount; i++)
    {
        // Calcula um desvio aleatório baseado na precisão apenas nos eixos X e Z
        Vector3 deviation = new Vector3(Random.Range(-1f, 1f) * (1f - config.accuracy), 0f, Random.Range(-1f, 1f) * (1f - config.accuracy));

        // Calcula a direção de disparo ajustada para o ponto de mira com desvio aleatório
        Vector3 shootDirection = (targetPosition - config.spawnPoint.position + deviation).normalized;

        // Instancia o objeto projetil a partir do prefab no ponto de origem
        GameObject projectile = Instantiate(config.projectilePrefab, config.spawnPoint.position, config.spawnPoint.rotation);

        // Obtém o componente Rigidbody do projetil
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Aplica a força para disparar o objeto
        rb.AddForce(shootDirection * config.shootForce, ForceMode.Impulse);
    }

    // Inicia o tempo de recarga
    StartCoroutine(Cooldown());
}
    IEnumerator Cooldown()
    {
        canShoot = false; // Impede que o personagem dispare enquanto estiver em recarga

        // Aguarda a duração do tempo de recarga
        yield return new WaitForSeconds(cooldownDuration);

        canShoot = true; // Permite que o personagem dispare novamente após o tempo de recarga
    }
}
