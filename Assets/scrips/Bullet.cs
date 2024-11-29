using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // Velocidad de la bala
    public float lifetime = 5f;     // Tiempo antes de destruir la bala

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("El prefab de la bala necesita un Rigidbody2D.");
            return;
        }

        // Mover la bala hacia adelante en la direcci�n en que est� orientada
        rb.velocity = transform.up * bulletSpeed;

        // Destruir la bala despu�s de un tiempo
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)

    {
        // Verificamos que la colisi�n sea con un objeto etiquetado como "Player"
        if (other.CompareTag("Player"))
        {
            // Obtener el script PlayerMovement del jugador afectado
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                // Reducir la vida del jugador
                player.health--;

                // Mostrar un mensaje de depuraci�n para verificar si el da�o se aplica correctamente
                Debug.Log($"El jugador ha sido golpeado. Salud restante: {player.health}");

                // Cambiar color a rojo temporalmente para indicar da�o
                StartCoroutine(FlashRed(player));

                // Si la salud del jugador es 0 o menor, llamamos al m�todo de muerte
                if (player.health <= 0)
                {
                    player.Die();
                }
            }
            // Destruir la bala despu�s de impactar
            Destroy(gameObject);
        }
        Debug.Log("holaaa");
    }

    // M�todo para cambiar temporalmente el color del jugador al ser golpeado por la bala
    IEnumerator FlashRed(PlayerMovement player)
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f); // Duraci�n del efecto
            spriteRenderer.color = Color.white;   // Restaurar color original
        }
    }
}
