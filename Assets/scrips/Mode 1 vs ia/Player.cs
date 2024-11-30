using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform transforBullet; // Punto de disparo
    public int health = 3; // Puntos de vida del jugador
    public AudioSource audioDisparo; // Audio del disparo

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private TankControls controls;
    private SpriteRenderer spriteRenderer;

    private float fireCooldown = 0.5f; // Cooldown entre disparos
    private float lastFireTime;

    void Awake()
    {
        controls = new TankControls(); // Inicializa los controles
    }

    void OnEnable()
    {
        controls.tank1.MoveTank1.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.tank1.MoveTank1.canceled += ctx => movementInput = Vector2.zero;
        controls.tank1.FireTank1.performed += _ => Fire();

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
        if (Time.time - lastFireTime >= fireCooldown) // Verificar enfriamiento
        {
            audioDisparo.Play();

            if (bulletPrefab != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, transforBullet.position, transforBullet.rotation);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                if (bulletRb != null)
                {
                    bulletRb.velocity = transform.right * 10f; // Velocidad de la bala
                }
            }

            lastFireTime = Time.time; // Actualizar último disparo
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(FlashRed()); // Cambiar color al recibir daño

        if (health <= 0)
        {
            Die();
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
        Debug.Log("Jugador eliminado");
        SceneManager.LoadScene("GameOver1");
    }
}
