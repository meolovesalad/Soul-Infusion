using UnityEngine;
using System;

public class PlayerSoul : MonoBehaviour
{
    [SerializeField] private int _transformPoint = 100;
    public int CurrentPoint { get; private set; } = 0;

    public event Action OnEnterAbyss;
    //public event Action OnExitAbyss;
    public event Action<int> OnPointChanged;

    private bool _isTransformed = false;

    public void AddPoint(int amount)
    {
        CurrentPoint += amount;

        OnPointChanged?.Invoke(CurrentPoint);

        CheckTransform();
    }

    private void CheckTransform()
    {
        if (_isTransformed) return;

        if (CurrentPoint >= _transformPoint)
        {
            EnterAbyss();
        }
    }

    private void EnterAbyss()
    {
        _isTransformed = true;
        CurrentPoint = 0;
        OnEnterAbyss?.Invoke();
    }


    public void ResetTransformation()
    {
        _isTransformed = false;
    }
}
