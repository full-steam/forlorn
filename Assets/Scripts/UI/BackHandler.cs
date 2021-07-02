using UnityEngine;
using UnityEngine.SceneManagement;

public class BackHandler : MonoBehaviour
{
    public GameObject quitPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    quitPanel.SetActive(true);
                    break;
                case 2:
                case 3:
                    GameManager.Instance.Blackboard.PausePanel.SetActive(true);
                    GameManager.Instance.Pause(true);
                    break;
            }
        }
    }
}
