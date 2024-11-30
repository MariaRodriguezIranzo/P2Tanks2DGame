using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOver2 : MonoBehaviour
{
    public Button jugarNuevo2;
    public Button exitButton;

    void Start()
    {
        jugarNuevo2.onClick.AddListener(OnJugar2NuevoButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }
    private void OnJugar2NuevoButtonClick()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic(); // Detener la música
        }
        // Inicia la carga de la escena Menu
        SceneManager.LoadScene("Level2");
    }

    private void OnExitButtonClick()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic(); // Detener la música
        }
        // Inicia la carga de la escena Creditos
        SceneManager.LoadScene("menu");
    }
}
