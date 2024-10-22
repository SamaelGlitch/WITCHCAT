using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    private MovimientoEnemigo enemyScript;

    private void Start()
    {
        // Obtener la referencia al script del enemigo en el GameObject padre
        enemyScript = GetComponentInParent<MovimientoEnemigo>();

        if (enemyScript == null)
        {
            Debug.LogError("No se encontró el script MovimientoEnemigo en el padre.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyScript.SetPlayerInRange(true);
            Debug.Log("Jugador detectado en el área.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyScript.SetPlayerInRange(false);
            Debug.Log("Jugador salió del área.");
        }
    }
}

