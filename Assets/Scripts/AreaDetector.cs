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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyScript.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyScript.SetPlayerInRange(false);
        }
    }
}

