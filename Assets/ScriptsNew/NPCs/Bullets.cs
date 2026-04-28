using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public static Bullets instance;
    public List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _amountToPool;

    private List<Rigidbody> _pooledRigidbodies = new List<Rigidbody>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        GameObject tmp;
        for (int i = 0; i < _amountToPool; i++)
        {
            tmp = Instantiate(_objectToPool);
            tmp.transform.parent = gameObject.transform;
            tmp.SetActive(false);

            pooledObjects.Add(tmp);
            _pooledRigidbodies.Add(tmp.GetComponent<Rigidbody>());
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public Rigidbody GetRigidbody(GameObject bullet)
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (bullet == pooledObjects[i])
            {
                return _pooledRigidbodies[i];
            }
        }
        return null;
    }
}
