using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Vector3 destination;

    public string[] flagToTrigger;
    public bool[] value;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetPositionAndRotation(destination, Quaternion.identity);
            for (int i = 0; i < flagToTrigger.Length; i++)
            {
                if (!string.IsNullOrEmpty(flagToTrigger[i]))
                {
                    GameManager.Instance.Blackboard.FlagManager.SetFlag(flagToTrigger[i], value[i]);
                }
            }
        }
    }
}
