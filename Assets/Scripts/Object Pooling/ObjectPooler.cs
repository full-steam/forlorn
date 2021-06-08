using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Component that handles object pooling.
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { set; get; }
    public List<ObjectPoolItem> poolItems;

    private List<GameObject> pooledObjects;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += ClearPooledObjects;
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.Blackboard.ObjectPooler = Instance;
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in poolItems)
        {
            for (int i = 0; i < item.poolSize; i++)
            {
                AddPoolObject(item.poolObjectPrefab);
            }
        }
    }

    /// <summary>
    /// Returns a pooled object of the given tag.
    /// </summary>
    /// <param name="tag">Tag of the requested pooled object.</param>
    /// <returns>A requested pooled object if available, null otherwise.</returns>
    public GameObject GetPooledObject(string tag)
    {
        Debug.Log(pooledObjects.Count);
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in poolItems)
        {
            if (item.poolObjectPrefab.tag == tag)
            {
                if (item.shouldExpand)
                {
                    return AddPoolObject(item.poolObjectPrefab);
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Instantiates a pooled object and adds it to the pooledObjects list.
    /// </summary>
    /// <param name="prefab">Prefab of the pooled object to be added.</param>
    /// <returns>The newly created pooled object.</returns>
    private GameObject AddPoolObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    private void ClearPooledObjects(Scene scene, LoadSceneMode mode)
    {
        pooledObjects.Clear();
    }
}
