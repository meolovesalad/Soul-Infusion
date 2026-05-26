using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField] private TransformVisualController _visual;
    [SerializeField] private TransformStatHandler _stats;
    [SerializeField] private TransformTimer _timer;
    [SerializeField] private PlayerSoul _playerSoul;

    private int _stage;

    private void OnEnable()
    {
        _playerSoul.OnEnterAbyss += EnterAbyss;
        _timer.OnTimerEnd += ExitAbyss;
    }

    private void OnDisable()
    {
        _playerSoul.OnEnterAbyss -= EnterAbyss;
        _timer.OnTimerEnd -= ExitAbyss;
    }

    public void EnterAbyss()
    {
        _stage = 1;

        _visual.EnterVisual();

        _stats.ApplyStats(_stage);

        _timer.StartTimer(10f);
    }

    public void ExitAbyss()
    {
        _visual.ExitVisual();

        _stats.RemoveStats();

        _timer.StopTimer();

        _playerSoul.ResetTransformation();
    }
}