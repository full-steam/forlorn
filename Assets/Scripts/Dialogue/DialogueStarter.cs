using System.Collections;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public float delay;

    private void OnEnable()
    {
        StartCoroutine(StartDialogue());
    }

    private IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(delay);

        GetComponent<Dialogue>().StartDialogue();

        yield return 0;
    }
}
