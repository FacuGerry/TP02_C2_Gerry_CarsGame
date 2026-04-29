using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public static Bullets instance;
    [SerializeField] private ObjectDataSO _data;
    [SerializeField] private GameObject _parent;

    private ObjectPooler _pool;
    private List<GameObject> _pooledObjects = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        _pool = GetComponent<ObjectPooler>();
    }

    private void Start()
    {
        _pool.CreatePool(_data.prefab, _parent, _data.spawnCount, _pooledObjects, false);
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _data.spawnCount; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }
}
