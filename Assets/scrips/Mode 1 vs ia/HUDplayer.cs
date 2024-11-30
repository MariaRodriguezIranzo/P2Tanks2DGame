using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDplayer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI player1Health;

    public Player player; // Referencia al jugador

    void Update()
    {
        if (player != null)
        {
            // Actualizar el texto con la salud actual del jugador
            player1Health.text = "Health: " + player.health;
        }
    }
}
