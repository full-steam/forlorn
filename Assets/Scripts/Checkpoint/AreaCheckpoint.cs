using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AreaCheckpoint : Checkpoint
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerCheckpoint();
    }
}
