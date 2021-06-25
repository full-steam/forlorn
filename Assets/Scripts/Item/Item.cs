using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    #region Asset Creation
    //[MenuItem("Assets/Create/Scriptable Objects/Item")]
    //public static void CreateMyAsset()
    //{
    //    var items = Resources.LoadAll<Item>("Item");
    //    int nextCount = items.Length + 1;

    //    Item asset = ScriptableObject.CreateInstance<Item>();
    //    asset.id = nextCount;
    //    AssetDatabase.CreateAsset(asset, "Assets/Resources/Item/" + (nextCount).ToString("0") + "-.asset");
    //    AssetDatabase.SaveAssets();
    //    EditorUtility.FocusProjectWindow();
    //    Selection.activeObject = asset;
    //}
    #endregion

    public int id;
    public new string name;
    public string text;
    public bool stackable;
    public bool usable;
    public ItemType type;
    [Tooltip("Each ID is bound to a specific value, please refer to Item.cs to check each ID's value.")]
    public ItemEffect[] effects;
    public Sprite icon;

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

//Changed to `Struct` instead
[System.Serializable]
public struct ItemEffect
{
    public int statID;
    public float value;

    //Status ID table:
    // 1   : Health
    // 2   : Hunger
}

public enum ItemType { Consumable, QuestItem };
