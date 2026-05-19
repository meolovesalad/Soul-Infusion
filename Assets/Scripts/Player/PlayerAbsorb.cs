using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbsorb : MonoBehaviour
{
    private InputSystem_Actions playerAction;

    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private float _absorbRange = 4f;
    private bool isSubscribed = false; // Cờ kiểm tra tránh đăng ký trùng lặp

    private void Start()
    {
        // Đổi sang Start để chắc chắn PlayerController.Instance đã được gán ở Awake
        TrySubscribeInput();
    }

    private void OnEnable()
    {
        // Dự phòng trường hợp Object bị Tắt/Bật (Disable/Enable) liên tục trong game
        TrySubscribeInput();
    }

    private void TrySubscribeInput()
    {
        if (isSubscribed) return;

        if (PlayerController.Instance != null)
        {
            playerAction = PlayerController.Instance.GetInputActions();
            if (playerAction != null)
            {
                playerAction.Player.Absorb.performed += OnAbsorbPerformed;
                isSubscribed = true;
            }
        }
    }

    private void OnDisable()
    {
        // Hủy đăng ký an toàn dựa trên cờ kiểm tra
        if (isSubscribed && playerAction != null)
        {
            playerAction.Player.Absorb.performed -= OnAbsorbPerformed;
            isSubscribed = false;
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _absorbRange);
    }
}