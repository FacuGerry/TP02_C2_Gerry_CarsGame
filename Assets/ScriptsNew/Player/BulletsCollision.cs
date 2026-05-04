using UnityEngine;

public class BulletsCollision : MonoBehaviour
{
    [SerializeField] private StatsDataSO _data;
    [SerializeField] private bool _isPlayerBullet;

    private void OnTriggerEnter(Collider collision)
    {
        if (_isPlayerBullet)
        {
            NpcHealthSystem npc = collision.gameObject.GetComponentInParent<NpcHealthSystem>();
            if (npc != null)
                npc.OnBulletShot_TakeDamage(_data.shootingDamage);
        }
        else
        {
            HealthSystem player = collision.gameObject.GetComponentInParent<HealthSystem>();
            if (player != null)
                player.TakeDamage(_data.shootingDamage);
        }

        gameObject.SetActive(false);
    }
}