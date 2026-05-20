using UnityEngine;
using System;

public class PlayerTransform : MonoBehaviour
{
    [SerializeField] private int _tranformPoint = 100;
    [SerializeField] private SpriteRenderer _playerColer;
    public int CurrentPoint { get; set; } = 0;

  /*  public event Action OnTransform;
    public event Action OnPointChange;*/

    private void Update()
    {
        AbyssForm();
    }
    private void AbyssForm()
    {
        if (CurrentPoint >= _tranformPoint)
        {
            _playerColer.color = Color.blue; 
        }
    }
}
