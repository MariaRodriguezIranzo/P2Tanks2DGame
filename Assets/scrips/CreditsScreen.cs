using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : MonoBehaviour
{
    public Button exitButton;
    public Button jugarNuevo;
    

    void Start()
    {
        exitButton.onClick.AddListener(OnExitButtonClick);
        jugarNuevo.onClick.AddListener(OnJugarNuevoButtonClick);
        
    }

    
    private void OnExitButtonClick()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic(); // Detener la m�sica
        }
        // Inicia la carga de la escena Creditos
        SceneManager.LoadScene("menu");
    }

    private void OnJugarNuevoButtonClick()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic(); // Detener la m�sica
        }
        // Inicia la carga de la escena Menu
        SceneManager.LoadScene("Level1");
    }

   
}
