using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _attackRange = 1.5f;

    [SerializeField] private float _attackCooldown = 1f;

    private bool _canAttack = true;

    private float _cooldownTimer;

    public float AttackRange => _attackRange;

    private IEnemyAttack _attackBehaviour;

    private void Awake()
    {
        _attackBehaviour =
            GetComponent<IEnemyAttack>();
    }
    private void Update()
    {
        HandleCooldown();
    }

    private void HandleCooldown()
    {
        if (_canAttack)
            return;

        _cooldownTimer -= Time.deltaTime;

        if (_cooldownTimer <= 0f)
        {
            _canAttack = true;
        }
    }

    public void SetTarget(Transform target)
    {
        if (_attackBehaviour is RangedAttack rangedAttack)
        {
            rangedAttack.SetTarget(target);
        }
    }

    public void Attack()
    {
        if (!_canAttack)
            return;

        _attackBehaviour.Attack();

        StartCooldown();
    }

    private void StartCooldown()
    {
        _canAttack = false;

        _cooldownTimer = _attackCooldown;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}