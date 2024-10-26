using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] private int maxHealth = 2; // Vida máxima del enemigo
    private int currentHealth; // Vida actual del enemigo

    [Header("Audio Settings")]
    [SerializeField] private AudioClip damageAudio; // Clip de sonido para el daño
    [SerializeField] private AudioClip dieAudio;
    private AudioSource audioSource; // Componente AudioSource

    private Player player; // Referencia al script del jugador

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Awake se ejecuta antes de que empiece el juego.
    private void Awake()
    {
        // Inicializar la vida actual del enemigo al máximo.
        currentHealth = maxHealth;

        // Encontrar al jugador en la escena
        player = FindObjectOfType<Player>(); // Asegúrate de que haya solo un jugador en la escena
    }

    // Método que se llama cuando otro objeto colisiona con este GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que ha colisionado es una bala
        if (other.gameObject.CompareTag("Bullet")) // Asegúrate de que tus balas tengan el tag "Bullet"
        {
            // Recibir daño (1 punto por impacto de bala)
            TakeDamage(1);

            // Destruir la bala al impactar
            Destroy(other.gameObject);
        }
    }

    // Función para reducir la salud del enemigo
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (damageAudio != null)
        {
            audioSource.PlayOneShot(damageAudio);
        }
        // Si la vida llega a 0, destruir al enemigo
        if (currentHealth <= 0)
        {
            PlayDeathSound();
            Die();
        }
    }
    private void PlayDeathSound()
    {
        if (dieAudio != null)
        {
            // Crea un objeto temporal de AudioSource en la posición actual del enemigo
            GameObject tempAudioObject = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();
            tempAudioSource.clip = dieAudio;
            tempAudioSource.Play();

            // Inicia la corrutina para silenciar el audio después de 0.5 segundos
            StartCoroutine(FadeOutAndDestroy(tempAudioSource, 0.5f));
        }
    }

    private IEnumerator FadeOutAndDestroy(AudioSource source, float duration)
    {
        yield return new WaitForSeconds(duration);
        source.volume = 0; // Baja el volumen a 0 después de 0.5 segundos
        Destroy(source.gameObject); // Destruye el objeto temporal
    }
    
    // Función que destruye al enemigo
    private void Die()
    {
        // Destruir al enemigo
        Destroy(gameObject);
    }
}