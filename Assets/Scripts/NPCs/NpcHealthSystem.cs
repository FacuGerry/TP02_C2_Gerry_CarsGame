using UnityEngine;

public class NpcHealthSystem : MonoBehaviour
{
    [SerializeField] private StatsDataSO _data;

    private ScoreController _score;

    private NpcController _controller;
    private int _life;

    private void Awake()
    {
        _score = MyPoolManager.Instance.gameObject.GetComponent<ScoreController>();
        _controller = GetComponent<NpcController>();
    }

    private void Start()
    {
        _life = _data.life;
    }

    private void TakeDamage(int damage)
    {
        //SfxManager.Instance.OnNpcDamaged_PlayClip();
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

        _score.OnNpcKilled_ChangeScore(_controller.isEnemy);

        gameObject.SetActive(false);
    }
}