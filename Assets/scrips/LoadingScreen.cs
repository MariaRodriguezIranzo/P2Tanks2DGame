using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Button playButton;
    public Button multiplayerPlay;
    public Button creditos;

    void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        multiplayerPlay.onClick.AddListener(OnMultiplayerPlayButtonClick);
        creditos.onClick.AddListener(OnCreditosButtonClick);
    }

    private void OnPlayButtonClick()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic(); // Detener la música
        }
        // Inicia la carga de la escena Level1
        SceneManager.LoadScene("Level1");
    }

    private void OnMultiplayerPlayButtonClick()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic(); // Detener la música
        }
        // Inicia la carga de la escena Multiplayer
        SceneManager.LoadScene("Level2");
    }

    private void OnCreditosButtonClick()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic(); // Detener la música
        }
        // Inicia la carga de la escena Creditos
        SceneManager.LoadScene("credits");
    }

}