using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public GameObject bulletPrefab; // Prefab de la bala
   
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private TankControls controls;

    void Awake()
    {
        controls = new TankControls(); // Inicializa los controles
    }

    void OnEnable()
    {
        if (gameObject.name == "player1")
        {
            controls.tank1.MoveTank1.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            controls.tank1.MoveTank1.canceled += ctx => movementInput = Vector2.zero;
            controls.tank1.FireTank1.performed += _ => Fire(); // Asignar disparo
        }
        else if (gameObject.name == "player2")
        {
            controls.tank2.MoveTank2.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            controls.tank2.MoveTank2.canceled += ctx => movementInput = Vector2.zero;
            controls.tank2.FireTank2.performed += _ => Fire(); // Asignar disparo
        }

        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 move = movementInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        if (movementInput.sqrMagnitude > 0) // Solo rota si hay movimiento
        {
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    void Fire()
    {
        if (bulletPrefab != null)
        {
            // Crear la bala en la posición del jugador
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                // Asignar velocidad a la bala
                bulletRb.velocity = transform.right * 10f; // Cambia a 'transform.up' si tu rotación apunta hacia arriba
            }
        }
    }

}
