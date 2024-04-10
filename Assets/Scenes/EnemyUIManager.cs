using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour
{
    public HealthUI enemyHealthUI;
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        // Obtém a referência para a câmera principal
        mainCamera = Camera.main;
    }

    private void Update()
    {

        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }

    // Método para atualizar a UI do inimigo
    public void UpdateEnemyHealthUI(int currentHealth, int maxHealth)
    {
        enemyHealthUI.UpdateHealthUI(currentHealth, maxHealth);
        // Mantém a rotação da UI do inimigo sempre voltada para a câmera

    }
}
