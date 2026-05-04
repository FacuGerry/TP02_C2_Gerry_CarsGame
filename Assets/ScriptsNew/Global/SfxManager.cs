using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource _sfx;
    [SerializeField] private AudioSource _ui;

    [Header("Player")]
    [SerializeField] private AudioClip _playerShootClip;
    [SerializeField] private AudioClip _playerSecondShoot;
    [SerializeField] private AudioClip _playerDamaged;
    [SerializeField] private AudioClip _playerDie;

    [Header("Enemies")]
    [SerializeField] private AudioClip _enemyShoot;
    [SerializeField] private AudioClip _enemyDamaged;
    [SerializeField] private AudioClip _enemyDie;

    [Header("UI")]
    [SerializeField] private AudioClip _btnHover;
    [SerializeField] private AudioClip _btnClick;

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
            Instance = null;
    }

    public void OnPlayerShoot_PlayClip()
    {
        _sfx.PlayOneShot(_playerShootClip);
    }

    public void OnPlayerSecondShoot_PlayClip()
    {
        _sfx.PlayOneShot(_playerSecondShoot);
    }

    public void OnEnemyShoot_PlayClip()
    {
        _sfx.PlayOneShot(_enemyShoot);
    }

    public void OnPlayerDamaged_PlayClip()
    {
        _sfx.PlayOneShot(_playerDamaged);
    }

    public void OnPlayerDie_PlayClip()
    {
        _sfx.PlayOneShot(_playerDie);
    }

    public void OnNpcDamaged_PlayClip()
    {
        _sfx.PlayOneShot(_enemyDamaged);
    }

    public void OnNpcDie_PlayClip(bool isEnemy)
    {
        _sfx.PlayOneShot(_enemyDie);
    }

    public void OnButtonHover_PlayClip()
    {
        _ui.PlayOneShot(_btnHover);
    }

    public void OnButtonClick_PlayClip()
    {
        _ui.PlayOneShot(_btnClick);
    }
}
