using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text healthText;
    public Player player;

    // Referencias a los cuatro elementos del Canvas
    public GameObject panel;        // Panel que deseas mostrar
    public Text gameOverText;       // Texto de Game Over
    public Button retryButton;      // Botón de reintentar
    public Button quitButton;       // Botón de salir

    private void Start()
    {
        // Inicialmente, desactiva el panel, texto y botones
        panel.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // Actualiza la salud en la UI y verifica el estado del jugador
        UpdateLivesUI();
    }

    private void Update()
    {
        UpdateLivesUI(); // Actualiza la UI cada frame
    }

    private void UpdateLivesUI()
    {
        if (player != null)
        {
            healthText.text = player.GetCurrentHealth().ToString() + " HP";
        }
        else
        {
            healthText.text = "0 HP";
            OnPlayerDestroyed(); // Activa los objetos del Canvas si el jugador es nulo
        }
    }

    // Método para activar los objetos del Canvas cuando el jugador es destruido
    private void OnPlayerDestroyed()
    {
        // Activa el panel, el texto y los dos botones
        panel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
}
