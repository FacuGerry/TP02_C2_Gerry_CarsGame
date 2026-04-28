using UnityEngine;

public class BuleltsCollision : MonoBehaviour
{
    [SerializeField] private StatsDataSO _data;
    [SerializeField] private bool _isPlayerBullet;

    private void OnTriggerEnter(Collider collision)
    {
        if (_isPlayerBullet)
        {
            if (collision.gameObject.TryGetComponent(out NpcHealthSystem npc))
                npc.OnBulletShot_TakeDamage(_data.shootingDamage);
        }
        else
        {
            if (collision.gameObject.TryGetComponent(out HealthSystem player))
                player.TakeDamage(_data.shootingDamage + _data.level);
        }

        gameObject.SetActive(false);
    }
}