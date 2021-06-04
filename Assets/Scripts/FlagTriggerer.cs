using System.Collections;
using UnityEngine;

public class FlagTriggerer : MonoBehaviour
{
    public string flag;
    public float delay;

    private void OnEnable()
    {
        StartCoroutine(TriggerFlag());
    }

    private IEnumerator TriggerFlag()
    {
        yield return new WaitForSeconds(delay);

        GameManager.Instance.Blackboard.FlagManager.SetFlag(flag, true);

        yield return 0;
    }
}
