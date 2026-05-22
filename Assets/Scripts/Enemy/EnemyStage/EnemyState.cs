public abstract class EnemyState
{
    protected EnemyBrain _brain;

    public EnemyState(EnemyBrain brain)
    {
        _brain = brain;
    }

    public virtual void Enter() { }

    public virtual void Update() { }

    public virtual void Exit() { }
}