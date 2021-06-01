using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUIHandler : MonoBehaviour
{
    public Image[] health;
    public Image[] hunger;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Blackboard.Player.playerStatus.SetIcons(health, hunger);
    }
}
