using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    có enable and disable để surcrice event mà thôi
*/
public class PlayerInputReader : MonoBehaviour
{
    public static PlayerInputReader Instance;

    private InputSystem_Actions actions;

    public event Action OnDash;
    public event Action OnAbsorb;
    public event Action OnAttack;

    public Vector2 MoveValue { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        actions = PlayerInputManager.Instance.Actions;
    }

    private void OnEnable()
    {
        if (actions == null) return;

        // MOVE
        actions.Player.Move.performed += MovePerformed;
        actions.Player.Move.canceled += MoveCanceled;

        // DASH
        actions.Player.Dash.performed += DashPerformed;

        // ABSORB
        actions.Player.Absorb.performed += AbsorbPerformed;

        // ATTACK
        actions.Player.Attack.performed += AttackPerformed;
    }

    private void OnDisable()
    {
        if (actions == null) return;

        // MOVE
        actions.Player.Move.performed -= MovePerformed;
        actions.Player.Move.canceled -= MoveCanceled;

        // DASH
        actions.Player.Dash.performed -= DashPerformed;

        // ABSORB
        actions.Player.Absorb.performed -= AbsorbPerformed;

        // ATTACK
        actions.Player.Attack.performed -= AttackPerformed;
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        MoveValue = ctx.ReadValue<Vector2>();
    }

    private void MoveCanceled(InputAction.CallbackContext ctx)
    {
        MoveValue = Vector2.zero;
    }

    private void DashPerformed(InputAction.CallbackContext ctx)
    {
        OnDash?.Invoke();
    }

    private void AbsorbPerformed(InputAction.CallbackContext ctx)
    {
        OnAbsorb?.Invoke();
    }

    private  void AttackPerformed(InputAction.CallbackContext ctx)
    {
        OnAttack?.Invoke();
    }
}