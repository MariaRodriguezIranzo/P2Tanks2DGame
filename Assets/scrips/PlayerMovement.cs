using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform transforBullet;
    public int health = 3; // Puntos de vida del jugador

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private TankControls controls;
    private SpriteRenderer spriteRenderer;

    private float fireCooldown = 5f; // Cooldown entre disparos
    private float lastFireTime;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            GameObject bullet = Instantiate(bulletPrefab, transforBullet.position, transforBullet.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                bulletRb.velocity = transform.right * 10f; // Velocidad de la bala
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // No es necesario revisar si el objeto es el jugador, ya que eso se maneja en el Bullet
        if (collision.gameObject.CompareTag("Player"))
        {
            health--;

            // Cambiar color a rojo temporalmente para indicar daño
            StartCoroutine(FlashRed());

            if (health <= 0)
            {
                Die();
            }
        }

        if (Time.time - lastFireTime >= fireCooldown)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f); // Duración del efecto
        spriteRenderer.color = Color.white;   // Restaurar color original
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        Destroy(gameObject);
    }
}