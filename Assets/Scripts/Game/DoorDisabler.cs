using UnityEngine;

public class DoorDisabler : MonoBehaviour
{
    public GameObject gate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            gate.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(true);
            gate.SetActive(true);
        }
    }
}
