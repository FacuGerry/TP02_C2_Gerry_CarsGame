using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public void CreatePool(GameObject prefab, GameObject parent, int quantity, List<GameObject> list, bool startActive)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.parent = parent.transform;
            list.Add(go);
            go.SetActive(startActive);
        }
    }
}
