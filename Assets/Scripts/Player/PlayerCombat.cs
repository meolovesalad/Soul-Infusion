using UnityEngine;
using UnityEngine.InputSystem;
using static BulletPoolManager;

public class PlayerCombat : MonoBehaviour
{
    private PlayerInputReader PlayerInput;
    [SerializeField] private GameObject firePositon;


    private void OnEnable()
    {
        PlayerInputReader.Instance.OnAttack += CastSpell;
    }

    private void OnDisable()
    {
        PlayerInputReader.Instance.OnAttack -= CastSpell;
    }

    private void CastSpell()
    {
        SpawnBullet(firePositon.transform, firePositon.transform.right, BulletPoolManager.Instance);
    }

    private void SpawnBullet(Transform tip, Vector2 direction, IPooling pool)
    {
        Bullet bullet = pool.GetBullet("normal bullet", tip.position, tip.rotation);
        if (bullet == null) return;

        bullet.SetDamage(20);   // truyền damage runtime vào đạn
        bullet.Activate(direction);
    }
}