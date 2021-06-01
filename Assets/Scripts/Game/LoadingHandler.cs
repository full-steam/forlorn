using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CheckLoadingFinished());
    }

    private IEnumerator CheckLoadingFinished()
    {
        yield return new WaitForEndOfFrame();
        do
        {
            yield return null;
        } while (GameManager.Instance.isLoading);
        Debug.Log("Loading finished");
        SceneManager.LoadScene("MainMenu");
    }
}
