using System.Collections.Generic;
using UnityEngine;

public class SpawnMines : MonoBehaviour
{
    [Header("Pool settings")]
    [SerializeField] private ObjectDataSO _data;

    [Header("Mine settings")]
    [SerializeField] private BoxCollider[] _spawnPlaces = new BoxCollider[0];

    private void Start()
    {
        int size = MyPoolManager.Instance.GetPoolSize<MineController>();

        for (int j = 0; j < size; j++)
        {
            MineController mine = MyPoolManager.Instance.GetInstanceFromPool<MineController>();
            BoxCollider coll = null;
            for (int i = 0; i < _spawnPlaces.Length; i++)
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
            Vector3 pos = bounds.center + randomOffset;

            mine.transform.position = pos;
            mine.Activate();
        }
    }
}
