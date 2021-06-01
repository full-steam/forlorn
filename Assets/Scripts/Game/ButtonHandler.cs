using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    private const string FIRST_LEVEL = "LevelScene";    //Change according to first scene name

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
        SceneManager.LoadScene(FIRST_LEVEL);
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

    public void SaveGame()
    {
        GameManager.Instance.SaveGame();
    }

    public void OpenDictionary()
    {
        GameManager.Instance.Blackboard.DictionaryPanel.SetActive(true);
    }
}
