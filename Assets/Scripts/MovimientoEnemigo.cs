using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimientoEnemigo : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform; // Referencia al Transform del jugador
    private bool isPlayerInRange = false; // Indica si el jugador está en el rango del enemigo

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Buscar al jugador en la escena
        Player player = FindObjectOfType<Player>();

        // Verificar si se encontró al jugador y obtener su Transform
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("No se encontró un jugador en la escena.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Solo sigue al jugador si está en el rango
        if (playerTransform != null && isPlayerInRange)
        {
            agent.SetDestination(playerTransform.position); // Mover hacia el jugador
        }
    }

    // Método público para establecer si el jugador está en rango
    public void SetPlayerInRange(bool inRange)
    {
        isPlayerInRange = inRange;
    }
}
