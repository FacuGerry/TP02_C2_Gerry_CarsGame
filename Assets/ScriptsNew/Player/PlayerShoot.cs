using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public event Action OnPlayerShoot;
    public event Action OnPlayerSecondShoot;

    [SerializeField] private KeyBindingsSO _keys;
    [SerializeField] private Transform _shootingPos;
    [SerializeField] private float _normalBulletDistance = 100f;

    [Header("Second bullet settings")]
    [SerializeField] private ObjectPooler _pool;
    [SerializeField] private GameObject _bulletsParent;
    [SerializeField] private ObjectDataSO _data;
    [SerializeField] private GameObject _cheatLine;

    private List<GameObject> _bullets = new List<GameObject>();
    private List<BulletMovement> _bulletMovements = new List<BulletMovement>();
    private bool _isShooting = false;
    private bool _startedShooting = false;

    private bool _isPaused = false;
    private IEnumerator _corroutineShoot;

    private Rigidbody _rb;

    private void Awake()
    {
        _pool.CreatePool(_data.prefab, _bulletsParent, _data.spawnCount, _bullets, false);

        foreach (GameObject bullet in _bullets)
            _bulletMovements.Add(bullet.GetComponent<BulletMovement>());

        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        PauseGame.OnPause += OnPause_PauseGame;
    }

    private void Update()
    {
        if (!_isPaused)
        {
            if (Input.GetKey(_keys.shoot))
                _isShooting = true;

            if (Input.GetKeyDown(_keys.secondShoot))
                SecondShoot();

            if (Input.GetKeyDown(_keys.showCheat))
                ShowCheat();

            if (Input.GetKeyUp(_keys.shoot))
            {
                _isShooting = false;
                _startedShooting = false;
            }

            if (_isShooting && !_startedShooting)
            {
                _startedShooting = true;
                if (_corroutineShoot != null)
                    StopCoroutine(_corroutineShoot);

                _corroutineShoot = Shooting();
                StartCoroutine(_corroutineShoot);
            }

            if (!_isShooting)
                if (_corroutineShoot != null)
                    StopCoroutine(_corroutineShoot);
        }
    }

    private void OnDisable()
    {
        PauseGame.OnPause -= OnPause_PauseGame;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Shooting()
    {
        while (_isShooting)
        {
            OnPlayerShoot?.Invoke();
            RaycastHit ray;
            if (Physics.Raycast(_shootingPos.position, transform.forward, out ray, _normalBulletDistance))
            {
                if (ray.collider != null && ray.collider.TryGetComponent(out NpcHealthSystem npc))
                {
                    npc.OnNormalShot_TakeDamage(_data.damage);
                    Debug.Log("Hit an NPC");
                }
                else
                    Debug.Log("you bad bro");
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void SecondShoot()
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (!_bullets[i].activeInHierarchy)
            {
                _bullets[i].SetActive(true);
                _bulletMovements[i].Shoot(_shootingPos, _data.travelDistance, _data.travelHeight, _data.travelDuration, gameObject, _rb.linearVelocity);
                OnPlayerSecondShoot?.Invoke();
                return;
            }
        }
    }

    private void ShowCheat()
    {
        _cheatLine.SetActive(!_cheatLine.activeInHierarchy);
    }

    private void OnPause_PauseGame(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
