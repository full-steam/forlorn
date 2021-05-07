using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentenceHolder : MonoBehaviour
{
    private List<ArrangementOption> tokens;

    // Start is called before the first frame update
    private void Start()
    {
        tokens = new List<ArrangementOption>();
    }

    public void AddWord(ArrangementOption option)
    {
        tokens.Add(option);
        option.transform.SetParent(transform);
        option.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void RemoveWord(ArrangementOption wordToRemove)
    {
        tokens.Remove(wordToRemove);
    }

    public string GetSentence()
    {
        string sent = "";

        int len = tokens.Count;

        for (int i = 0; i < len; i++)
        {
            if (i == len - 1) sent += tokens[i].Word;
            else sent += tokens[i].Word + " ";
        }

        return sent;
    }

    public void Reset()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<ArrangementOption>().Toggle();
        }
    }
}
