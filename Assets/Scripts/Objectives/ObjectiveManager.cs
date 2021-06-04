using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages objectives.
/// </summary>
public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives;

    public Dictionary<string, GameObject> objectiveTexts;

    // Start is called before the first frame update
    private void Start()
    {
        objectiveTexts = new Dictionary<string, GameObject>();
        
        GameManager.Instance.Blackboard.FlagManager.FlagTriggered += OnFlagTriggered;

        foreach (var objective in objectives)
        {
            if (GameManager.Instance.Blackboard.FlagManager.GetFlag(objective.startFlag))
            {
                if (!GameManager.Instance.Blackboard.FlagManager.GetFlag(objective.endFlag))
                {
                    AddObjective(objective.endFlag, objective.text);
                }
            }
        }
    }

    /// <summary>
    /// Listener to be run when a flag is triggered.
    /// </summary>
    /// <param name="flag">Name of the triggered flag.</param>
    private void OnFlagTriggered(string flag)
    {
        if (objectiveTexts.ContainsKey(flag))
        {
            RemoveObjective(flag);
        }

        foreach (var objective in objectives)
        {
            if (objective.startFlag == flag)
            {
                AddObjective(objective.endFlag, objective.text);
            }
        }
    }

    /// <summary>
    /// Adds a new objective.
    /// </summary>
    /// <param name="endFlag">Flag that will end the objective.</param>
    /// <param name="text">Text of the objective.</param>
    private void AddObjective(string endFlag, string text)
    {
        if (!objectiveTexts.ContainsKey(endFlag))
        {
            GameObject newObjective = GameManager.Instance.Blackboard.ObjectPooler.GetPooledObject("objective");
            newObjective.transform.SetParent(transform);
            newObjective.transform.localScale = Vector3.one;

            newObjective.GetComponentInChildren<TMP_Text>().text = text;

            newObjective.SetActive(true);

            objectiveTexts.Add(endFlag, newObjective);
        }
    }

    /// <summary>
    /// Remove an objective.
    /// </summary>
    /// <param name="endFlag">End flag of the objective.</param>
    private void RemoveObjective(string endFlag)
    {
        Debug.Log("RemoveObjective called");
        if (objectiveTexts.ContainsKey(endFlag))
        {
            GameObject temp = objectiveTexts[endFlag];
            objectiveTexts.Remove(endFlag);
            temp.SetActive(false);
        }
    }
}
