using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public static PositionManager Instance;

    private Vector3 _previousPosition;
    private Vector3 _currentPosition;

    private Vector3 _rotation;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public Vector3 GetPreviousPosition()
    {
        return _previousPosition;
    }

    public Vector3 GetCurrentPosition()
    {
        return _currentPosition;
    }

    public Vector3 GetCurrentRotation()
    {
        return _rotation;
    }

    public void SetNewValues(Vector3 position, Vector3 rotation)
    {
        _previousPosition = _currentPosition;
        _currentPosition = position;
        _rotation = rotation;
    }
}