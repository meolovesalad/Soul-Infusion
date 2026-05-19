using UnityEngine;

public class Bullet : MonoBehaviour, IAttackable
{
    [Header("Bullets stats")]
    [SerializeField] protected float speed = 10f;
    //[SerializeField] protected float damage = 20f;
    [SerializeField] protected float lifeTime = 2f;

    // damage KHÔNG còn SerializeField vì sẽ được set runtime từ Gun
    protected float damage = 20f;

    protected float timer = 0f;
    protected Rigidbody2D rb;
    System.Action<Bullet> returnToPool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    public void SetDamage(float value) => damage = value;

    // Update is called once per frame
    void Update()
    {
        BulletLife();
    }

    protected virtual void BulletLife()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ReturnToPool();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Damage logic here
        //ReturnToPool();
        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null)
        {
            Attack(target);
            ReturnToPool(); // Hủy đạn sau khi trúng
        }
    }

    // ── Public API ────────────────────────────────────────────────────────

    /// <summary>Set damage từ RuntimeGunStats trước khi Activate.</summary>

    public void Activate(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    public void SetReturnToPool(System.Action<Bullet> action)
    {
        returnToPool = action;
    }


    protected void ReturnToPool()
    {
        rb.linearVelocity = Vector2.zero;
        returnToPool?.Invoke(this);
    }

    public virtual void Attack(IDamageable target)
    {
        target.TakeDamage(damage);
    }
}
