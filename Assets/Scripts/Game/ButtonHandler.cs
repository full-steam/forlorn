using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        GameManager.Instance.LoadGame();
    }

    public void StartNewGame()
    {
        //TODO: Implement New Game
        Debug.LogWarning("\"New Game\" not implemented yet");
    }

    public void Pause()
    {
        GameManager.Instance.Pause();
    }

    public void Pause(bool pause)
    {
        GameManager.Instance.Pause(pause);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PlaySound(string audioID)
    {
        AudioController.Play(audioID);
    }

    public void PlayMusic(string songID)
    {
        AudioController.PlayMusic(songID);
    }
}
