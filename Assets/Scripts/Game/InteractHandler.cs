using UnityEngine;
using UnityEngine.UI;

public class InteractHandler : MonoBehaviour
{
    private Button button;
    private PlayerInteraction player;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        player = GameManager.Instance.Blackboard.Player.playerInteraction;
        player.InteractButton = button;
        button.onClick.AddListener(player.Interact);
    }
}
