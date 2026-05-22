using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 3f;

    private Transform _target;
    private Rigidbody2D _enemyRb;

    private Vector2 _moveDirection;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform target)
    { _target = target; }

    public void MoveToTarget()
    {
        if (_target == null)
        {
            return;
        }
        _moveDirection = (_target.position - transform.position).normalized;
        _enemyRb.linearVelocity = _moveDirection * _enemySpeed;
    }

    public void Stop()
    {
        _enemyRb.linearVelocity = Vector2.zero;
    }

    public float DistanceToTarget()
    {
        return Vector2.Distance(
            transform.position,
            _target.position);
    }


}
