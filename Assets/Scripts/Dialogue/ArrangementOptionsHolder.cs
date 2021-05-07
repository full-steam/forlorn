using System.Collections.Generic;
using UnityEngine;

public class ArrangementOptionsHolder : MonoBehaviour
{
    public SentenceHolder sentenceHolder;

    public void AddOption(string option)
    {
        ArrangementOption arrangementOption = GameManager.Instance.Blackboard.ObjectPooler.GetPooledObject("arrangement option").GetComponent<ArrangementOption>();
        arrangementOption.Init(option, this);
        arrangementOption.transform.SetParent(transform);
    }

    public void SetOptions(List<string> options)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (string option in options) AddOption(option);
    }
}
