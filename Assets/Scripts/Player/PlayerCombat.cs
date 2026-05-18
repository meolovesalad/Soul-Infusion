using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private InputSystem_Actions playerAction;

    private void Start()
    {
        playerAction = PlayerController.Instance.GetInputActions();

        playerAction.Player.Attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        if (playerAction != null)
        {
            playerAction.Player.Attack.performed -= OnAttackPerformed;
        }
    }

    private void OnAttackPerformed(InputAction.CallbackContext ctx)
    {
        Debug.Log("ATTACK");
    }
}