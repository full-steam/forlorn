using UnityEngine;

/// <summary>
/// Data for objects that need to be pooled using ObjectPooler.
/// </summary>
[System.Serializable]
public class ObjectPoolItem
{
    public GameObject poolObjectPrefab;
    public int poolSize;
    public bool shouldExpand;
}