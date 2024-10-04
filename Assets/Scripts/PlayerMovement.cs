using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public Rigidbody rb;

    private Vector3 movement;
    public Animator animator;

    void Start()
    {
        // Obtener el componente Rigidbody y Animator
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Obtener la entrada del jugador (WASD o flechas)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //Animación de correr
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetFloat("Speed",Mathf.Abs (moveVertical));
        // Crear el vector de movimiento
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Si hay movimiento, actualizar la animación de correr
        if (movement.magnitude > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void FixedUpdate()
    {
        // Aplicar el movimiento al Rigidbody
        if (movement.magnitude > 0)
        {
            MoveCharacter(movement);
        }
    }

    void MoveCharacter(Vector3 direction)
    {
        // Movimiento del personaje
        Vector3 moveOffset = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveOffset);

        // Rotación del personaje hacia la dirección de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }
}
