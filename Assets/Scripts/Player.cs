using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private int maxHealth = 100; // Vida máxima del jugador
    private int currentHealth; // Vida actual del jugador

    [Header("Health Regeneration")]
    [SerializeField] private int healthRegenAmount = 25; // Cantidad de salud restaurada
    [SerializeField] private float healthRegenRate = 1.0f; // Intervalo de regeneración en segundos

    private bool isInvulnerable = false; // Indica si el jugador es invulnerable (después de recibir daño)

    private Animator animator; // Referencia al Animator del jugador
    public GameManager gameManager;

    [Header("Damage Settings")]
    [SerializeField] private float damageCooldown = 1.0f; // Tiempo en segundos entre cada daño
    private float nextDamageTime = 0f; // Tiempo de espera para el próximo daño

    [Header("Audio Settings")]
    [SerializeField] private AudioClip laserShoot; // Clip de sonido para el disparo
    [SerializeField] private AudioClip hitHurt_cat; // Clip de sonido para el daño
    private AudioSource audioSource; // Componente AudioSource

    private void Start()
    {
        // Inicializar la salud del jugador.
        currentHealth = maxHealth;
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Obtener el Animator en el hijo del jugador.
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró un Animator en el hijo del jugador. Por favor, añade uno.");
        }

        StartCoroutine(RegenerateHealth());
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
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

        if (laserShoot != null)
        {
            audioSource.PlayOneShot(laserShoot);
        }

        Destroy(bullet, 0.5f);

        // Actualizar el tiempo para el próximo disparo.
        nextFireTime = Time.time + fireRate;
    }



    // Función que maneja el daño recibido por el jugador y aplica un empuje
    private void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            if (hitHurt_cat != null)
            {
                audioSource.PlayOneShot(hitHurt_cat);
            }
            
            // Si la salud llega a 0 o menos, el jugador muere.
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {

            }
        }
    }

    // Función que destruye al jugador cuando muere.
    private void Die()
    {
        // Aquí puedes añadir lógica adicional para manejar la muerte del jugador (reiniciar el nivel, mostrar un menú, etc.)
        
        Destroy(gameObject);
    }

    private IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(healthRegenRate); // Espera el intervalo de regeneración

            if (currentHealth < maxHealth)
            {
                currentHealth += healthRegenAmount;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth; // Asegura que no exceda la salud máxima
                }
            }
        }
    }

    // Detectar colisión con el enemigo.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && Time.time >= nextDamageTime)
        {
            TakeDamage(25); // Define cuánto daño recibe el jugador
            nextDamageTime = Time.time + damageCooldown; // Establece el próximo tiempo en que se puede recibir daño
        }
    }
}
