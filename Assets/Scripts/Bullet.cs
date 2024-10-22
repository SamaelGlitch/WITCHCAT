using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto con el que colisiona tiene el tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Destruir la bala cuando toque al enemigo
            Destroy(gameObject);

            // Aquí puedes añadir lógica adicional si el enemigo recibe daño o cualquier otra acción
            // Por ejemplo, podrías llamar a un método en el enemigo para reducir su salud.
        }
    }
}