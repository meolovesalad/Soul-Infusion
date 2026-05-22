public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyBrain brain): base(brain)
    {
    }

    public override void Update()
    {
        _brain.Movement.MoveToTarget();

        if (_brain.Movement.DistanceToTarget()
            <= _brain.Combat.AttackRange)
        {
            _brain.ChangeState(_brain.AttackState);
        }
    }
}