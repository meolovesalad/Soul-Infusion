using UnityEngine;
using System;

public class PlayerSoul : MonoBehaviour
{
    [SerializeField] private int _transformPoint = 100;
    public int CurrentPoint { get; private set; } = 0;

    public event Action OnEnterAbyss;
    public event Action OnExitAbyss;
    public event Action<int> OnPointChanged;

    private bool transformed = false;

    public void AddPoint(int amount)
    {
        CurrentPoint += amount;

        OnPointChanged?.Invoke(CurrentPoint);

        CheckTransform();
    }

    private void CheckTransform()
    {
        if (transformed) return;

        if (CurrentPoint >= _transformPoint)
        {
            EnterAbyss();
        }
        else if (CurrentPoint < _transformPoint) 
        {
            ExitAbyss();
        }
    }

    private void EnterAbyss()
    {
        transformed = true;
        OnEnterAbyss?.Invoke();
    }

    private void ExitAbyss()
    {
        transformed = false;
        OnExitAbyss?.Invoke();
    }
}
