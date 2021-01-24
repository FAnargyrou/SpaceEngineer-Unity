using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioManager audioManager;

    void Start()
    {
        if (!audioManager)
            Debug.LogWarning("Unable to play theme song; Play manager was not assigned to " + name);
        else
            audioManager.Play("Theme");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
