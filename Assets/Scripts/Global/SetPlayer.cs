using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    [SerializeField] private SelectionsSO _selection;
    
    private void Awake()
    {
        _selection.ResetCar();

        GameObject car = Instantiate(_selection.car.prefab, transform.position, transform.rotation);

        _selection.spawnedCar = car;
    }
}