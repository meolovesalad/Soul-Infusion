using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class EnemyBrain : MonoBehaviour
{
    [field: SerializeField]
    public EnemyMovement Movement { get; private set; }

    [field: SerializeField]
    public EnemyCombat Combat { get; private set; }

    private EnemyState _currentState;

    private EnemyChaseState _chaseState;
    private EnemyAttackState _attackState;

    [SerializeField]
    private Transform _target;

    private void Awake()
    {
        _chaseState = new EnemyChaseState(this);
        _attackState = new EnemyAttackState(this);
    }


    private void Start()
    {
        // set deffault stage is chase stage
        Movement.SetTarget(_target);
        Combat.SetTarget(_target);
        ChangeState(_chaseState);
    }

    private void Update()
    {
        _currentState?.Update();
    }

    public void ChangeState(EnemyState newState)
    {
        _currentState?.Exit();

        _currentState = newState;

        _currentState.Enter();
    }

    public EnemyChaseState ChaseState => _chaseState;
    public EnemyAttackState AttackState => _attackState;

}
