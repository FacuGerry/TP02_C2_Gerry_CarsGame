using System;
using UnityEngine;

public class NpcHealthSystem : MonoBehaviour
{
    public static event Action<bool> OnNpcDie;
    public static event Action OnNpcDamaged;

    [SerializeField] private StatsDataSO _data;
    [SerializeField] private PlayerShoot _player;

    private NpcController _controller;
    private int _life;

    private void Awake()
    {
        _controller = GetComponent<NpcController>();
    }

    private void Start()
    {
        _life = _data.life * ((_data.level / 10) + 1);
    }

    private void TakeDamage(int damage)
    {
        OnNpcDamaged?.Invoke();
        _life -= damage;
        if (_life <= 0)
        {
            _life = 0;
            NpcDie();
        }
    }

    public void OnNormalShot_TakeDamage(int damage)
    {
        TakeDamage(damage);
    }

    public void OnBulletShot_TakeDamage(int damage)
    {
        TakeDamage(damage);
    }

    private void NpcDie()
    {
        if (_controller.isEnemy)
            Debug.Log("Killed an enemy");
        else
            Debug.Log("Killed a citizen");

        OnNpcDie?.Invoke(_controller.isEnemy);

        gameObject.SetActive(false);
    }
}