using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hudMultiplayer : MonoBehaviour
{
   public TMPro.TextMeshProUGUI player1Health;
   public TMPro.TextMeshProUGUI player2Health;


    void Update()
    {
       if (GameObject.Find("player1") != null)
       {
            player1Health.text = "Player 1 Health: " + GameObject.Find("player1").GetComponent<PlayerMovement>().health;
       }
        if (GameObject.Find("player2") != null)
        {
            player2Health.text = "Player 2 Health: " + GameObject.Find("player2").GetComponent<PlayerMovement>().health;
        }

    }
}
