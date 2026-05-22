using UnityEngine;
using System;

public class PlayerTransform : MonoBehaviour
{
    [SerializeField] private int _transformPoint = 100;
    [SerializeField] private SpriteRenderer _playerRenderer;
    public int CurrentPoint { get; private set; } = 0;

    public event Action OnTransform;
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
            transformed = true;

            _playerRenderer.color = Color.blue;

            OnTransform?.Invoke();
        }
    }
}
