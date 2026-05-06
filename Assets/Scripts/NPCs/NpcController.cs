using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private SelectionsSO _selection;
    [SerializeField] private Animator _anim;
    [SerializeField] private StatsDataSO _data;
    private GameObject _player;

    [Header("Bullets")]
    [SerializeField] private Transform _bulletShootPos;

    private List<EnemyStates> _states = new List<EnemyStates>();
    private EnemyStates currentState;
    private Rigidbody _rb;

    public bool isEnemy;

    private bool _isShooting;

    private float _shootingSpeed;

    private IEnumerator _corroutineShoot;

    private void Awake()
    {
        _player = _selection.spawnedCar;

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

    private void Update()
    {
        if (!PauseGame.Instance.IsPaused)
        {
            currentState?.OnUpdate();

            if (isEnemy)
                CheckForPlayer();
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Shooting(Transform startPos, float height, float duration, GameObject player)
    {
        while (_isShooting)
        {
            BulletEnemy bullet = MyPoolManager.Instance.GetInstanceFromPool<BulletEnemy>();
            GameObject go = bullet.gameObject;
            go.SetActive(true);

            Vector3 start = startPos.position;

            CarMovement car = player.GetComponent<CarMovement>();
            Vector3 playerVelocity = car.GetSpeed();
            playerVelocity.y = 0f;

            Vector3 futurePos = player.transform.position + playerVelocity;

            float time = 0f;
            while (time < duration)
            {
                float t = time / duration;

                Vector3 pos = Vector3.Lerp(start, futurePos, t);

                float yOffset = height * t * 4f * (1f - t);
                pos.y += yOffset;

                go.transform.position = pos;

                time += Time.deltaTime;
                yield return null;
            }

            go.SetActive(false);

            SfxManager.Instance.OnEnemyShoot_PlayClip();

            yield return new WaitForSeconds(_shootingSpeed);
        }
    }

    public void EnableShooting(bool isShooting)
    {
        if (isShooting)
        {
            if (_corroutineShoot != null) { }
            else
            {
                _isShooting = true;

                _corroutineShoot = Shooting(_bulletShootPos, _data.heightToShoot, _data.bulletSpeed, _player);
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
}
