using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public static event Action OnNpcShoot;

    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _anim;
    [SerializeField] private StatsDataSO _data;

    [Header("Bullets")]
    [SerializeField] private Transform _bulletShootPos;

    private List<EnemyStates> _states = new List<EnemyStates>();
    private EnemyStates currentState;
    private Rigidbody _rb;

    public bool isEnemy;

    private bool _isShooting;

    private float _shootingSpeed;

    private IEnumerator _corroutineShoot;
    private bool _isPaused = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _states.Add(new StateIdle());
        _states.Add(new StateShoot());

        foreach (EnemyStates state in _states)
            state.Initialize(_anim, _rb, this, _player);

        currentState = FindState(StateType.Idle);
        currentState.OnEnter();

    }

    private void Start()
    {
        SwitchState(FindState(StateType.Idle));

        _shootingSpeed = _data.shootingSpeed;
    }

    private void OnEnable()
    {
        PauseGame.OnPause += OnPause_PauseGame;
    }

    private void Update()
    {
        if (!_isPaused)
        {
            if (currentState != null)
                currentState.OnUpdate();

            if (isEnemy)
                CheckForPlayer();
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
            GameObject bullet = Bullets.instance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = _bulletShootPos.position;
                bullet.SetActive(true);

                Vector3 bulletDirection = (_player.transform.position - bullet.transform.position).normalized;

                Debug.Log("Enemy shot a bullet to (" + bulletDirection.x + ", " + bulletDirection.y + ", " + bulletDirection.z + ")");
            }
            OnNpcShoot?.Invoke();
            yield return new WaitForSeconds(_shootingSpeed);
        }
        yield return null;
    }

    public void EnableShooting(bool isShooting)
    {
        if (isShooting)
        {
            if (_corroutineShoot != null) { }
            else
            {
                _isShooting = true;

                _corroutineShoot = Shooting();
                StartCoroutine(_corroutineShoot);
            }
        }
        else
        {
            _isShooting = false;

            if (_corroutineShoot != null)
                StopCoroutine(_corroutineShoot);

            _corroutineShoot = null;
        }
    }

    private void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= _data.distanceToShoot)
            SwitchState(FindState(StateType.Shoot));
        else
            SwitchState(FindState(StateType.Idle));
    }

    private void SwitchState(EnemyStates newState)
    {
        if (currentState == newState)
            return;

        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    private EnemyStates FindState(StateType stateToFind)
    {
        foreach (EnemyStates state in _states)
            if (state.state == stateToFind)
                return state;

        return null;
    }

    private void OnPause_PauseGame(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
