using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento
    public Vector2 moveDirection = Vector2.left; // Dirección inicial
    public bool isPatrolling = true; // Si el enemigo patrulla
    public float patrolDistance = 5f; // Distancia de patrullaje
    private Vector2 startPoint; // Punto de inicio del patrullaje

    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // Punto de disparo
    public float fireRate = 1.5f; // Tiempo entre disparos
    private float lastFireTime;

    public int health = 3; // Puntos de vida del enemigo
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPoint = transform.position; // Guardar la posición inicial
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        if (isPatrolling)
        {
            float distanceFromStart = Vector2.Distance(startPoint, transform.position);
            if (distanceFromStart >= patrolDistance)
            {
                moveDirection = -moveDirection; // Cambiar dirección al llegar al borde del rango
            }
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime); // Aplicar movimiento
    }

    private void HandleShooting()
    {
        if (Time.time - lastFireTime >= fireRate)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }

    private void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                bulletRb.velocity = firePoint.right * 7f; // Ajustar velocidad de la bala
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(FlashRed());

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

    private void Die()
    {
        Debug.Log("Enemigo destruido");
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1); // Infligir daño al jugador
            }
        }
    }
}
