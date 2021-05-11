using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to hold the data and logic of the sentence holder.
/// </summary>
public class SentenceHolder : MonoBehaviour
{
    private List<ArrangementOption> tokens;

    // Start is called before the first frame update
    private void Start()
    {
        tokens = new List<ArrangementOption>();
    }

    /// <summary>
    /// Adds a word from the options holder to the sentence.
    /// </summary>
    /// <param name="option">Option to be added.</param>
    public void AddWord(ArrangementOption option)
    {
        tokens.Add(option);
        option.transform.SetParent(transform);
        option.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// Removes a word from the list of sentence tokens.
    /// Only removes it from the private list.
    /// Moving it to the options holder happens in ArrangementOptionsHolder.
    /// </summary>
    /// <param name="wordToRemove">Option to be removed.</param>
    public void RemoveWord(ArrangementOption wordToRemove)
    {
        tokens.Remove(wordToRemove);
    }

    /// <summary>
    /// Returns the sentence as a string with spaces between each token.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Clears the sentence and returns all options to the options holder.
    /// </summary>
    public void Reset()
    {
        tokens.Clear();
        foreach (Transform child in transform)
        {
            child.GetComponent<ArrangementOption>().Toggle();
        }
    }
}
