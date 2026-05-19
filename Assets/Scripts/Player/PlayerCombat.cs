using UnityEngine;
using UnityEngine.InputSystem;
using static BulletPoolManager;

public class PlayerCombat : MonoBehaviour
{
    private InputSystem_Actions playerAction;
    [SerializeField] private GameObject firePositon;

    private void Start()
    {
        playerAction = PlayerController.Instance.GetInputActions();

        playerAction.Player.Attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        if (playerAction != null)
        {
            playerAction.Player.Attack.performed -= OnAttackPerformed;
        }
    }

    private void OnAttackPerformed(InputAction.CallbackContext ctx)
    {
        Debug.Log("ATTACK");
        SpawnBullet(firePositon.transform, firePositon.transform.right, BulletPoolManager.Instance);
    }

    protected void SpawnBullet(Transform tip, Vector2 direction, IPooling pool)
    {
        Bullet bullet = pool.GetBullet("normal bullet", tip.position, tip.rotation);
        if (bullet == null) return;

        bullet.SetDamage(20);   // truyền damage runtime vào đạn
        bullet.Activate(direction);
    }
}