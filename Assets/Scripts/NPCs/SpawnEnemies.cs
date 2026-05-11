using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [Header("Enemy spawners")]
    [SerializeField] private BoxCollider[] _spawnPlaces = new BoxCollider[0];
    [SerializeField] private SelectionsSO _selection;
    
    private void Start()
    {
        if (_selection.gameMode == GameModes.Competitive)
        {
            int size = MyPoolManager.Instance.GetPoolSize<NpcController>();

            for (int j = 0; j < size; j++)
            {
                NpcController enemy = MyPoolManager.Instance.GetInstanceFromPool<NpcController>();
                BoxCollider coll = null;
                for (int i = 0; i < _spawnPlaces.Length; i++)
                {
                    float rand = Random.value;
                    if (rand <= (i / 10f))
                    {
                        coll = _spawnPlaces[i];
                        break;
                    }
                }

                if (coll == null)
                    coll = _spawnPlaces[0];

                Bounds bounds = coll.bounds;
                Vector3 randomOffset = new Vector3(Random.Range(-bounds.extents.x, bounds.extents.x), bounds.extents.y, Random.Range(-bounds.extents.z, bounds.extents.z));
                Vector3 pos = bounds.center + randomOffset;

                enemy.Activate();
                enemy.transform.SetPositionAndRotation(pos, coll.gameObject.transform.rotation);
            }
        }
    }
}
