using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbsorb : MonoBehaviour
{
    private InputSystem_Actions playerAction;
    private LayerMask itemLayer;
    [SerializeField] private float _absorbRange = 4f;

    private void Start()
    {
        playerAction = PlayerController.Instance.GetInputActions();

        playerAction.Player.Attack.performed += OnAbsorbPerformed;
    }

    private void OnDisable()
    {
        if (playerAction != null)
        {
            playerAction.Player.Attack.performed -= OnAbsorbPerformed;
        }
    }

    private void OnAbsorbPerformed(InputAction.CallbackContext ctx) => PullItems();

    private void PullItems()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
           transform.position,
            _absorbRange,
           itemLayer
       );

        foreach (Collider2D hit in hits)
        {
            Soul soul = hit.GetComponent<Soul>();
            if (soul != null)
            {
                soul.SetTarget(transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _absorbRange);
    }

}
