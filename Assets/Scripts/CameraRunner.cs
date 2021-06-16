using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 offset;

    private void Awake()
    {
        GameManager.Instance.Blackboard.Camera = gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }
}
