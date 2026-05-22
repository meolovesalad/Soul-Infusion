using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float _attackCooldown = 1f;
    private float _timer;

    public EnemyAttackState(EnemyBrain brain)
        : base(brain)
    {
    }

    public override void Enter()
    {
        _timer = _attackCooldown;

        _brain.Movement.Stop();

        _brain.Combat.Attack();
    }

    public override void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _brain.ChangeState(_brain.ChaseState);
        }
    }
}