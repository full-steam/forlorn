using UnityEngine;

public class PausePanelAssigner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Blackboard.PausePanel = gameObject;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Blackboard.PausePanel = null;
    }
}
