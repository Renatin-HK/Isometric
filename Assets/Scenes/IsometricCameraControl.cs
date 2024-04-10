using UnityEngine;

public class IsometricCameraControl : MonoBehaviour
{
    public GameObject mapa; // Referência para o objeto do mapa na cena
    [Range(1f, 10f)] public float rotationSpeed = 5f; // Velocidade de rotação da câmera, com um mínimo de 1 e máximo de 10

    private bool isRotating = false; // Flag para controlar se a câmera está rotacionando
    private Quaternion targetRotation; // Rotação alvo da câmera
    private float targetAngle; // Ângulo alvo de rotação

    void Start()
    {
        // Posiciona a câmera centralizando o mapa
        PositionCamera();
    }

    void Update()
    {
        // Verifica se a tecla Q foi pressionada para rotacionar no sentido anti-horário
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetAngle -= 90f; // Define o ângulo alvo para uma rotação de -90 graus
            isRotating = true; // Habilita a rotação
        }
        // Verifica se a tecla E foi pressionada para rotacionar no sentido horário
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetAngle += 90f; // Define o ângulo alvo para uma rotação de 90 graus
            isRotating = true; // Habilita a rotação
        }

        // Se a câmera estiver rotacionando, chama o método de rotação suave
        if (isRotating)
        {
            SmoothRotateCamera();
        }
    }

    // Método para posicionar a câmera centralizando o mapa
    void PositionCamera()
    {
        // Calcula a posição da câmera para centralizar o mapa
        Vector3 mapCenter = mapa.transform.position;
        Vector3 cameraPosition = new Vector3(mapCenter.x, mapCenter.y + Mathf.Sqrt(2) * mapa.transform.localScale.y / 2f, mapCenter.z);
        
        // Define a posição da câmera
        transform.position = cameraPosition;

        // Define a rotação da câmera para 45 graus em x e y
        transform.rotation = Quaternion.Euler(45f, 0, 0f);
    }

    // Método para rotacionar suavemente a câmera para o ângulo alvo
    void SmoothRotateCamera()
    {
        // Calcula a rotação alvo com base no ângulo alvo
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);
        
        // Rotaciona suavemente a câmera para a rotação alvo
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Se a câmera estiver próxima o suficiente da rotação alvo, finaliza a rotação
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            isRotating = false;
            transform.rotation = targetRotation; // Garante que a rotação seja exatamente o alvo para evitar pequenos desvios
        }
    }
}
