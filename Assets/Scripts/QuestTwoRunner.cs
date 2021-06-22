using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTwoRunner : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.Blackboard.FlagManager.CheckFlags();
    }
}
