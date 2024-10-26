using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text healthText;
    public Player player;

    public GameObject panel;
    public Text gameOverText;
    public Button retryButton;
    public Button quitButton;

    private bool isCursorLocked = true; // Controla el estado del cursor

    private void Start()
    {
        panel.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        LockCursor();
        UpdateLivesUI();
    }

    private void Update()
    {
        UpdateLivesUI();

        // Alterna el cursor con ESC solo si el jugador está vivo
        if (player != null && Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;
            if (isCursorLocked)
                LockCursor();
            else
                UnlockCursor();
        }
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
            OnPlayerDestroyed();
        }
    }

    private void OnPlayerDestroyed()
    {
        UnlockCursor(); // Muestra el cursor cuando el jugador muere
        panel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}