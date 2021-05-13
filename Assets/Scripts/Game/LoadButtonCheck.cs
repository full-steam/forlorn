using UnityEngine;
using UnityEngine.UI;

public class LoadButtonCheck : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("HasSaveData") == 1) GetComponent<Button>().interactable = true;
    }
}
