using UnityEngine;

public class DialogueUtilities : MonoBehaviour
{
    public void OnDialogueEnd()
    {
        GameManager.Instance.Blackboard.Player.playerMovement.ToggleMovement(true);
    }
}
