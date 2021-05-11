using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to handle sentence arrangement options.
/// </summary>
public class ArrangementOptionsHolder : MonoBehaviour
{
    public SentenceHolder sentenceHolder;

    /// <summary>
    /// Adds an option to the sentence arrangement options.
    /// </summary>
    /// <param name="option">Option to be displayed.</param>
    public void AddOption(string option)
    {
        ArrangementOption arrangementOption = GameManager.Instance.Blackboard.ObjectPooler.GetPooledObject("arrangement option").GetComponent<ArrangementOption>();
        arrangementOption.Init(option, this);
        arrangementOption.transform.SetParent(transform);
    }

    /// <summary>
    /// Sets up the initial options.
    /// </summary>
    /// <param name="options">Options to be displayed.</param>
    public void SetOptions(List<string> options)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (string option in options) AddOption(option);
    }
}
