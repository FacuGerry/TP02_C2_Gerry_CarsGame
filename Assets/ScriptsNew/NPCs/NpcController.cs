using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public static event Action OnNpcShoot;

    public List<Vector3> positions = new List<Vector3>();
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _placeToAim;
    [SerializeField] private Animator _anim;
    [SerializeField] private StatsDataSO _data;

    [Header("Gun")]
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Transform _walkPos;
    [SerializeField] private Transform _shootPos;

    [Header("Bullets")]
    [SerializeField] private Transform _bulletShootPos;

    private List<EnemyStates> _states = new List<EnemyStates>();
    private EnemyStates currentState;
    private Rigidbody _rb;
    private NavMeshAgent _agent;

    public bool isEnemy;

    private bool _isShooting;

    private float _shootingSpeed;

    private IEnumerator _corroutineShoot;
    private bool _isPaused = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();

        _states.Add(new StateIdle());
        _states.Add(new StateWalk());
        _states.Add(new StateShoot());

        foreach (EnemyStates state in _states)
            state.Initialize(_anim, _rb, this, _agent, _player);

        currentState = FindState(StateType.Idle);
        currentState.OnEnter();

    }

    private void Start()
    {
        transform.position = new Vector3(positions[0].x, transform.position.y, positions[0].z);

        SwitchState(FindState(StateType.Walk));

        _agent.speed = _data.speed;
        _shootingSpeed = _data.shootingSpeed - (_data.level / 10);
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

            MoveGun();
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
            Rigidbody rb = Bullets.instance.GetRigidbody(bullet);
            if (bullet != null)
            {
                bullet.transform.position = _bulletShootPos.position;
                bullet.SetActive(true);

                Vector3 playerPos = _placeToAim.transform.position;
                Vector3 bulletDirection = (playerPos - bullet.transform.position).normalized;

                rb.linearVelocity = bulletDirection * _data.shootingSpeed;

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
            SwitchState(FindState(StateType.Walk));
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

    private void MoveGun()
    {
        if (currentState == FindState(StateType.Walk))
        {
            _weapon.transform.position = _walkPos.position;
            _weapon.transform.localEulerAngles = _walkPos.localEulerAngles;
        }
        if (currentState == FindState(StateType.Shoot))
        {
            _weapon.transform.position = _shootPos.position;
            _weapon.transform.localEulerAngles = _shootPos.localEulerAngles;
        }
    }

    private void OnPause_PauseGame(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
