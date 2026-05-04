using UnityEngine;

public class BulletEnemy : MonoBehaviour, IPooleable
{
    public bool IsActive { get; set; }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
    }
}