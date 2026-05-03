using System;
using System.Collections.Generic;
using UnityEngine;

public class MyPoolManager : MonoBehaviour
{
    [SerializeField] private PoolSettingSO _prefabs;
    private Dictionary<Type, List<IPooleable>> _pooleablesDictionary;

    private void Start ()
    {
        //InitializePool();
    }

    /*private void InitializePool ()
    {
        // Recorremos el diccionario de pooleables y creamos 10 de cada uno
        foreach (KeyValuePair<Type, List<IPooleable>> item in _pooleablesDictionary)
        {
            MonoBehaviour prefab = FindPrefab(item.Key);
            if (prefab != null)
                CreatePool(prefab.gameObject, transform, 10, item.Value);
        }
    }

    public void CreatePool (GameObject prefab, Transform parent, int quantity, List<IPooleable> list)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.parent = parent;
            IPooleable pooleable = go.GetComponent<IPooleable>();
            pooleable.DeActivate();
            list.Add(pooleable);
        }
    }

    private MonoBehaviour FindPrefab<T> (T component)
    {
        MonoBehaviour prefab = null;
        for (int i = 0; i < _prefabs.poolSettings.Count; i++)
        {
            if (_prefabs.poolSettings[i].prefab.GetComponent<T>() != null)
            {
                prefab = _prefabs.poolSettings[i].prefab;
                break;
            }
        }

        return prefab;
    }*/

    public T GetInstanceFromPool<T> () where T : MonoBehaviour
    {
        foreach (KeyValuePair<Type, List<IPooleable>> item in _pooleablesDictionary)
        {
            if (typeof(T) == item.Key)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (!item.Value[i].IsActive)
                    {
                        return (T)item.Value[i];
                    }
                }

                break;
            }
        }

        return null;
    }
}