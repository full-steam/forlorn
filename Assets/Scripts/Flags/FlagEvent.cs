using UnityEngine;
/// <summary>
/// An event associated with a flag on an object.
/// </summary>
[System.Serializable]
public class FlagEvent
{
    public enum FlagEventType
    {
        EnableObject
    };

    public string flag;
    public FlagEventType eventType;

    public GameObject target;

    /// <summary>
    /// Executes the event associated with the flag on the object.
    /// </summary>
    public void ExecuteEvent()
    {
        switch (eventType)
        {
            case FlagEventType.EnableObject:
                target.SetActive(true);
                break;
        }
    }
}
