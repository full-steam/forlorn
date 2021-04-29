using UnityEngine;
using UnityEngine.UI;

// TODO: ADD CREATE COMPONENT
public class Item : ScriptableObject
{
    public int id;
    public new string name;
    public string text;
    public bool stackable;
    public bool usable;
    public ItemType type;
    public ItemEffect[] effects;
    public Image icon;

    public string GetTypeString()
    {
        switch (type)
        {
            case (ItemType.Consumable): return "Consumable";
            case (ItemType.QuestItem): return "Quest Item";
            default: return "";
        }
    }
}

//Change to `Struct` instead?
[System.Serializable]
public class ItemEffect
{
    public int statID;
    public float value;

    ///Status ID table:
    /// 1   : Health
    /// 2   : Hunger
}

public enum ItemType { Consumable, QuestItem };
