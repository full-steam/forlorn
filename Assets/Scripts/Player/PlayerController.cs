using UnityEngine;

[RequireComponent(typeof(PlayerInteraction), typeof(PlayerMovement), typeof(PlayerStatus))]
public class PlayerController : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public PlayerInteraction playerInteraction;
    public PlayerStatus playerStatus;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInteraction = GetComponent<PlayerInteraction>();
        playerStatus = GetComponent<PlayerStatus>();
        GameManager.Instance.Blackboard.Player = this;
    }
}
