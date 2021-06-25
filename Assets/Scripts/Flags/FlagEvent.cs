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
        MoveObject,
        FadeOutAndIn,
        RunDialogue,
        ChangeSprite,
        SetTag,
        SetColliderTrigger,
        ModifyMoney,
        REMOVE_RING
    };

    [System.Serializable]
    public class MoveTarget
    {
        public GameObject target;
        public Vector3 coords;
    }

    public string flag;
    public FlagEventType eventType;
    public float delay = 1.0f;
    public bool foldout = false;
    public bool foldoutTargets = false;
    public string tag;
    public bool isTrigger = false;
    public int amount = 0;

    public List<GameObject> targets;
    public List<MoveTarget> moveTargets;
    public CameraFadeManager.Options fadeOutOptions;
    public CameraFadeManager.Options fadeInOptions;
    public Dialogue dialogueComponent;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    /// <summary>
    /// Executes the event associated with the flag on the object.
    /// </summary>
    public void ExecuteEvent()
    {
        switch (eventType)
        {
            case FlagEventType.EnableObject:
                foreach (GameObject target in targets)
                {
                    if (target) target.SetActive(true);
                }
                break;
            case FlagEventType.DisableObject:
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
            case FlagEventType.FadeOutAndIn:
                //GameManager.Instance.Blackboard.Camera.GetComponent<CameraFadeManager>().FadeOutAndIn(fadeOutOptions, fadeInOptions, delay);
                break;
            case FlagEventType.RunDialogue:
                dialogueComponent.StartDialogue();
                break;
            case FlagEventType.ChangeSprite:
                spriteRenderer.sprite = sprite;
                break;
            case FlagEventType.SetTag:
                foreach (GameObject target in targets)
                {
                    target.tag = tag;
                }
                break;
            case FlagEventType.SetColliderTrigger:
                foreach (GameObject target in targets)
                {
                    target.GetComponent<Collider2D>().isTrigger = isTrigger;
                }
                break;
            case FlagEventType.REMOVE_RING:
                List<ItemObject> itemList = GameManager.Instance.Blackboard.Player.GetComponent<PlayerStatus>().itemList;

                for (int i = 0; i < itemList.Count; i++)
                {
                    if (itemList[i].itemID == 3)
                    {
                        GameManager.Instance.Blackboard.Player.GetComponent<PlayerStatus>().itemList.RemoveAt(i);
                        return;
                    }
                }
                break;
            case FlagEventType.ModifyMoney:
                GameManager.Instance.Blackboard.Player.GetComponent<PlayerStatus>().ModifyMoney(amount);
                break;
        }
    }
}
