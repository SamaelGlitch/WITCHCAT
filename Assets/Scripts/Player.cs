using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Shooting")] // Atributos visibles de Disparo
    [SerializeField] private GameObject bulletPrefab; // Prefab de la bala
    [SerializeField] private Transform shootPoint; // Punto de origen del disparo
    [SerializeField] private float shootForce = 20f; // Fuerza del disparo
    [SerializeField] private float fireRate = 0.5f; // Cadencia de disparo (0.5 segundos entre disparos)
    private float nextFireTime = 0f; // Tiempo que debe esperar antes de poder disparar de nuevo

    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 10; // Vida máxima del jugador
    private int currentHealth; // Vida actual del jugador

    private bool isInvulnerable = false; // Indica si el jugador es invulnerable (después de recibir daño)
    private float invulnerabilityDuration = 1f; // Duración de la invulnerabilidad en segundos

    private Animator animator; // Referencia al Animator del jugador

    private void Start()
    {
        // Inicializar la salud del jugador.
        currentHealth = maxHealth;

        // Obtener el Animator en el hijo del jugador.
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró un Animator en el hijo del jugador. Por favor, añade uno.");
        }
    }

    private void Update()
    {
        // Detectar si se ha presionado el botón derecho del mouse (clic derecho).
        if (Input.GetMouseButton(1) && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Reproducir la animación "magic" si existe
        if (animator != null)
        {
            animator.SetTrigger("magic");
        }

        // Instanciar la bala en el punto de disparo con la misma rotación que el jugador.
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Obtener el Rigidbody de la bala y asegurarnos que tenga uno.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Aplicar la fuerza en la dirección del punto de disparo hacia adelante.
            rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        }

        Destroy(bullet, 3f);

        // Actualizar el tiempo para el próximo disparo.
        nextFireTime = Time.time + fireRate;
    }

    // Función que maneja el daño recibido por el jugador y aplica un empuje
    private void TakeDamage(int damage, Vector3 hitDirection)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            Debug.Log("Player hit! Health: " + currentHealth);

            // Si la salud llega a 0 o menos, el jugador muere.
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                // Activar la invulnerabilidad temporalmente.
                StartCoroutine(InvulnerabilityCoroutine());
            }
        }
    }

    // Corrutina que controla el tiempo de invulnerabilidad
    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    // Función que destruye al jugador cuando muere.
    private void Die()
    {
        Debug.Log("Player is dead!");
        // Aquí puedes añadir lógica adicional para manejar la muerte del jugador (reiniciar el nivel, mostrar un menú, etc.)
        Destroy(gameObject);
    }

    // Detectar colisión con el enemigo.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) // Asegúrate de que los enemigos tengan el tag "Enemy"
        {
            Vector3 hitDirection = (transform.position - other.transform.position).normalized;
            TakeDamage(1, hitDirection); // El jugador recibe 1 punto de daño y es empujado.
        }
    }
}
