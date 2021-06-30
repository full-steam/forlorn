using UnityEngine;

public class PausePanelAssigner : MonoBehaviour
{

    void Start()
    {
        //Debug.Log("Assigning Pause Panel");
        // IF PAUSE PANEL IS APPARENT AT START OF LOADING LEVEL SCENE, DISABLE THE PAUSE PANEL IN PREFAB
        GameManager.Instance.Blackboard.PausePanel = gameObject;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.Blackboard.PausePanel = null;
    }
}
