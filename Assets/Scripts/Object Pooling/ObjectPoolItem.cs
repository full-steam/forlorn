using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolItem : MonoBehaviour
{
    public GameObject pooledObjectPrefab;
    public int size;
    public bool shouldExpand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject()
    {
        Debug.LogWarning("ObjectPoolItem.GetPooledObject is not implemented");
        return new GameObject();
    }
}
