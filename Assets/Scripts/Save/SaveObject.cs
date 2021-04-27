using UnityEngine;

[System.Serializable]
public class SaveObject
{
    // ---Player Data
    public float health;
    public float hunger;
    public float deltaDistance;
    public string sceneName;
    public int steps;
    public bool starving;
    public Vector3 posInScene;
    //public ItemObject[] itemList;             //uncomment later after implementing Item

    // ---Flags
    public string[] keys;
    public bool[] values;
}
