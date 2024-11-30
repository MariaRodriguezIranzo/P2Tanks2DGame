using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 1; // Daño que inflige la bala
    public float lifetime = 5f; // Tiempo antes de destruir la bala

    void Start()
    {
        Destroy(gameObject, lifetime); // Autodestruir bala después del tiempo límite
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage); // Reducir salud del jugador
            }

            Destroy(gameObject); // Destruir bala tras impactar
        }
        
    }
}