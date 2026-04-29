using System.Collections.Generic;
using UnityEngine;

public class SpawnMines : MonoBehaviour
{
    [Header("Pool settings")]
    [SerializeField] private ObjectPooler _pool;
    [SerializeField] private GameObject _minePrefab;
    [SerializeField] private GameObject _mineParent;
    [SerializeField] private int _spawnCount = 20;
    private List<GameObject> _mines = new List<GameObject>();

    [Header("Mine settings")]
    [SerializeField] private List<BoxCollider> _spawnPlaces = new List<BoxCollider>();

    private void Awake()
    {
        _pool.CreatePool(_minePrefab, _mineParent, _spawnCount, _mines, true);
    }

    private void Start()
    {
        foreach (GameObject mine in _mines)
        {
            BoxCollider coll = null;
            for (int i = 0; i < _spawnPlaces.Count; i++)
            {
                float rand = Random.value;
                if (rand >= 0.5f)
                {
                    coll = _spawnPlaces[i];
                    break;
                }
            }

            if (coll == null)
                coll = _spawnPlaces[0];

            Bounds bounds = coll.bounds;
            Vector3 randomOffset = new Vector3(Random.Range(-bounds.extents.x, bounds.extents.x), 0f, Random.Range(-bounds.extents.z, bounds.extents.z));
            Vector3 pos = coll.transform.TransformPoint(randomOffset);

            mine.transform.position = randomOffset;
        }
    }
}
