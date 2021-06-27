using UnityEngine;
using UnityEngine.UI;

public class SaveDataCheck : MonoBehaviour
{

    public bool isLoadButton;
    public GameObject newGamePromptPanel;

    private bool hasSaveData = false;

    void Start()
    {
        if (PlayerPrefs.GetInt("HasSaveData") == 1) hasSaveData = true;
        if (isLoadButton && hasSaveData) GetComponent<Button>().interactable = true;
    }

    public void ProcessNewGame()
    {
        if (hasSaveData)
        {
            newGamePromptPanel.SetActive(true);
        }
        else
        {
            GetComponent<ButtonHandler>().NewGame();
        }
    }
}
