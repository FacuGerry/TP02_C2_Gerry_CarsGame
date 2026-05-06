using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private KeyBindingsSO _keys;
    [SerializeField] private Transform _shootingPos;

    [Header("Second bullet settings")]
    [SerializeField] private ObjectDataSO _data;
    [SerializeField] private GameObject _cheatLine;

    private bool _isShooting = false;
    private bool _startedShooting = false;

    private IEnumerator _corroutineShoot;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!PauseGame.Instance.IsPaused)
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Shooting()
    {
        while (_isShooting)
        {
            //SfxManager.Instance.OnPlayerShoot_PlayClip();
            if (Physics.Raycast(_shootingPos.position, _shootingPos.forward, out RaycastHit ray, _data.shootingDistance))
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
        BulletMovement bullet = MyPoolManager.Instance.GetInstanceFromPool<BulletMovement>();
        if (bullet == null)
            return;
        bullet.Activate();
        bullet.Shoot(_shootingPos, _data.travelDistance, _data.travelHeight, _data.travelDuration, gameObject, _rb.linearVelocity);
        SfxManager.Instance.OnPlayerSecondShoot_PlayClip();
    }

    private void ShowCheat()
    {
        _cheatLine.SetActive(!_cheatLine.activeInHierarchy);
    }
}
