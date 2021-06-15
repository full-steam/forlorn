using UnityEngine;
using UnityEngine.Tilemaps;

public class GatesDisabler : MonoBehaviour
{
    public TilemapRenderer gateTower;

    private new TilemapRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<TilemapRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            renderer.enabled = false;
            gateTower.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            renderer.enabled = true;
            gateTower.enabled = true;
        }
    }
}
