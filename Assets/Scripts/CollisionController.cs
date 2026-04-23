using System;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public event Action<int> OnPlayerCrashed;

    private void OnCollisionEnter(Collision collision)
    {
        if ((int)collision.relativeVelocity.magnitude < 1)
            return;

        OnPlayerCrashed?.Invoke((int)collision.relativeVelocity.magnitude);
        Debug.Log("car collided");
    }
}
