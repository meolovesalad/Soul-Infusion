using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerInputReader PlayerInput;
    [SerializeField] private GameObject firePositon;


    private void Start()
    {
        PlayerInputReader.Instance.OnAttack += CastSpell;
    }

    private void OnDisable()
    {
        PlayerInputReader.Instance.OnAttack -= CastSpell;
    }

    private void CastSpell()
    {
        // lật bằng scale không thật sự đổi trục nên oahir làm vậy
        Vector2 direction =
        transform.localScale.x > 0
        ? Vector2.right
        : Vector2.left;

        //Debug.Log(direction);

        SpawnBullet(
             firePositon.transform,
             direction,
             BulletPoolManager.Instance
         );
    }

    private void SpawnBullet(Transform tip, Vector2 direction, IPooling pool)
    {
        Bullet bullet = pool.GetBullet("normal bullet", tip.position, tip.rotation);
        if (bullet == null) return;

        bullet.SetDamage(20);   // truyền damage runtime vào đạn
        bullet.Activate(direction);
    }
}