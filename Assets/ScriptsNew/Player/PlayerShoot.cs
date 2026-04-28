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

    [Header("Second bullet stats")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _bulletsToCreate = 20;
    [SerializeField] private float _bulletDuration = 2f;
    [SerializeField] private float _bulletDistance = 10f;
    [SerializeField] private int _bulletDamage = 50;
    [SerializeField] private GameObject _cheatLine;

    private List<GameObject> _bullets = new List<GameObject>();
    private List<BulletMovement> _bulletMovements = new List<BulletMovement>();
    private bool _isShooting = false;
    private bool _startedShooting = false;

    private bool _isPaused = false;
    private IEnumerator _corroutineShoot;

    private void Awake()
    {
        for (int i = 0; i < _bulletsToCreate; i++)
        {
            _bullets.Add(_bulletPrefab);
        }

        foreach (GameObject bullet in _bullets)
            _bulletMovements.Add(bullet.GetComponent<BulletMovement>());
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
            if (Physics.Raycast(_shootingPos.transform.position, transform.forward, out ray, _normalBulletDistance))
                if (ray.collider != null && ray.collider.TryGetComponent(out NpcHealthSystem npc))
                {
                    npc.OnNormalShot_TakeDamage(_bulletDamage);
                    Debug.Log("Hit an NPC");
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
                // NEW METHOD
                _bullets[i].SetActive(true);
                _bulletMovements[i].Shoot(_shootingPos.position, _bulletDistance, _bulletDuration, gameObject);
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
