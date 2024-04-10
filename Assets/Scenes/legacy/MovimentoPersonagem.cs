using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField]
    public float moveSpeed = 5f; // Velocidade de movimento do personagem
    public float dashSpeed = 10f; // Velocidade de dash do personagem
    public float dashDuration = 0.5f; // Duração do dash em segundos
    public float dashCooldown = 2f; // Tempo de recarga do dash em segundos
    public float rotationSpeed = 5f; // Velocidade de rotação do personagem
    public float jumpForce = 10f; // Força do pulo
    public float groundDistance = 0.2f; // Distância para verificar se o jogador está no chão
    public LayerMask groundMask; // Máscara de camada para definir quais camadas representam o chão
    private bool isGrounded; // Flag para controlar se o jogador está no chão
    private Camera cam;
    private Rigidbody rb; // Referência para o componente Rigidbody


    private bool isDashing = false; // Flag para verificar se o personagem está dando um dash
    private bool canDash = true; // Flag para verificar se o personagem pode dar um dash

    void Start()
    {
        // Obtém a câmera principal
        cam = Camera.main;

        // Obtém o componente Rigidbody do personagem
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Verifica se o personagem está tentando dar um dash

        // Move o personagem de acordo com a entrada e a velocidade de movimento
        // rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        MoveCharacter();
        // Jump();
    }

    // void MoveCharacter()
    // {
    //     // Obtém a entrada do jogador para movimento
    //     float inputHorizontal = Input.GetAxisRaw("Horizontal");
    //     float inputVertical = Input.GetAxisRaw("Vertical");

    //     // Calcula a direção de movimento baseada na entrada do jogador
    //     Vector3 moveDirection = new Vector3(inputHorizontal, 0f, inputVertical).normalized;

    //     // Converte a direção de movimento para o espaço da tela
    //     Vector3 screenDirection = cam.transform.TransformDirection(moveDirection);
    //     screenDirection.y = 0f; // Garante que o movimento seja apenas no plano da tela

    //     // Calcula a rotação desejada
    //     Quaternion desiredRotation = Quaternion.LookRotation(screenDirection);

    //     // Rotação suave do personagem na direção em que está se movendo
    //     if (screenDirection != Vector3.zero)
    //     {
    //         // Calcula a rotação suave
    //         Quaternion newRotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, rotationSpeed * Time.fixedDeltaTime);
    //         rb.MoveRotation(newRotation);
    //     }

    //     // Aplica o movimento relativo à tela ao personagem usando Rigidbody
    //     rb.MovePosition(rb.position + screenDirection * moveSpeed * Time.fixedDeltaTime);
    // }

    void MoveCharacter()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");


        // Calcula a direção de movimento baseada na entrada do jogador
        Vector3 moveDirection = new Vector3(inputHorizontal, 0f, inputVertical).normalized;

        // Converte a direção de movimento para o espaço da tela
        Vector3 screenDirection = cam.transform.TransformDirection(moveDirection);
        screenDirection.y = 0f; // Garante que o movimento seja apenas no plano da tela

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = cam.transform.position.y; // Define a profundidade com base na altura da câmera

        Vector3 worldMousePosition = cam.ScreenToWorldPoint(mousePosition);

        // Calcula a direção entre o jogador e a posição do mouse no plano isométrico
        Vector3 lookDirection = worldMousePosition - transform.position;
        lookDirection.y = 0f; // Mantém o jogador no mesmo plano y

        Quaternion desiredRotation = Quaternion.LookRotation(lookDirection);

        // Calcula a rotação suave
        Quaternion newRotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(newRotation);

        // Aplica o movimento relativo à tela ao personagem usando Rigidbody
        rb.MovePosition(rb.position + screenDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    IEnumerator Dash()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        isDashing = true; // Define que o personagem está dando um dash
        canDash = false; // Impede que o personagem dê outro dash enquanto estiver em recarga

        // Obtém a direção de movimento atual do personagem
        Vector3 moveDirection = new Vector3(inputHorizontal, 0f, inputVertical).normalized;

        // Executa o dash movendo o personagem em uma direção com uma velocidade maior
        // rb.velocity = transform.forward * dashSpeed;

        rb.AddForce(moveDirection * dashSpeed, ForceMode.Impulse);

        // Aguarda a duração do dash
        yield return new WaitForSeconds(dashDuration);

        // Reseta a velocidade do personagem após o término do dash
        rb.velocity = Vector3.zero;

        // Aguarda o tempo de recarga do dash
        yield return new WaitForSeconds(dashCooldown);

        isDashing = false; // Permite que o personagem dê outro dash
        canDash = true; // Permite que o personagem dê outro dash após o tempo de recarga
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Jump();

        // Lança um raio para baixo para verificar se o jogador está no chão
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundDistance, groundMask);
        Debug.DrawRay(transform.position, -Vector3.up * groundDistance, isGrounded ? Color.green : Color.red);  // Desenha a linha de debug para o raio
    }
}
