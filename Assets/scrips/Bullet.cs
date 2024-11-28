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

        // Mover la bala hacia adelante en la dirección en que está orientada
        rb.velocity = transform.up * bulletSpeed;

        // Destruir la bala después de un tiempo
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir la bala al colisionar con cualquier objeto
        Destroy(gameObject);
    }
}
