using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        exitButton.onClick.AddListener(OnExitButtonClick);
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
