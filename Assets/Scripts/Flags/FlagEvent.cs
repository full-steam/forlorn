using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// An event associated with a flag on an object.
/// </summary>
[System.Serializable]
public class FlagEvent
{
    public enum FlagEventType
    {
        EnableObject,
        DisableObject,
        MoveObject
    };

    [System.Serializable]
    public class MoveTarget
    {
        public GameObject target;
        public Vector3 coords;
    }

    public string flag;
    public FlagEventType eventType;

    public List<GameObject> targets;
    public List<MoveTarget> moveTargets;

    /// <summary>
    /// Executes the event associated with the flag on the object.
    /// </summary>
    public void ExecuteEvent()
    {
        Debug.Log("Execute event called!");
        switch (eventType)
        {
            case FlagEventType.EnableObject:
                Debug.Log("ENABLE OBJECT");
                foreach (GameObject target in targets)
                {
                    target.SetActive(true);
                }
                break;
            case FlagEventType.DisableObject:
                Debug.Log("DISABLE OBJECT");
                foreach (GameObject target in targets)
                {
                    target.SetActive(false);
                }
                break;
            case FlagEventType.MoveObject:
                foreach (MoveTarget target in moveTargets)
                {
                    target.target.transform.SetPositionAndRotation(target.coords, target.target.transform.localRotation);
                }
                break;
        }
    }
}
