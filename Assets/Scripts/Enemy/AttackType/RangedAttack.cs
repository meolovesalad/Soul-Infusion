using UnityEngine;

public class RangedAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private Transform _firePosition;

    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void Attack()
    {
        if (_target == null)
            return;

        Vector2 direction =
            (_target.position - _firePosition.position).normalized;

        SpawnBullet(
            _firePosition,
            direction,
            BulletPoolManager.Instance
        );
    }



    private void SpawnBullet( Transform tip, Vector2 direction,IPooling pool)
    {
        Bullet bullet =
            pool.GetBullet(
                "enemy bullet",
                tip.position,
                tip.rotation);

        if (bullet == null)
            return;

        bullet.SetDamage(10);

        bullet.Activate(direction);
    }
}