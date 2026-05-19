using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private Rigidbody2D playerRD;
    private InputSystem_Actions inputActions;
    private Vector2 moveInput;
    private bool isDashing;

    public InputSystem_Actions GetInputActions() => inputActions;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _dashSpeed = 20f;
    [SerializeField] private float _dashDuration = 0.2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerRD = GetComponent<Rigidbody2D>();
        // Khởi tạo Input Action ngay tại Awake
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        if (inputActions != null)
        {
            inputActions.Enable();
            inputActions.Player.Dash.performed += OnDashformed;
        }
    }

    private void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Player.Dash.performed -= OnDashformed;
            inputActions.Disable(); // Nên Unsubscribe sự kiện TRƯỚC KHI Disable InputActions
        }
    }

    private void OnDashformed(InputAction.CallbackContext ctx)
    {
        if (!isDashing)
        {
            StartCoroutine(PlayerDash());
        }
    }

    void Update()
    {
        if (inputActions != null)
        {
            moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            MoveAround();
        }
    }

    private void MoveAround()
    {
        playerRD.linearVelocity = moveInput.normalized * _moveSpeed;
    }

    private IEnumerator PlayerDash()
    {
        isDashing = true;

        // Lưu lại hướng di chuyển hiện tại lúc bấm nút để Dash thẳng theo hướng đó, 
        // Tránh việc đổi hướng đột ngột giữa chừng khi đang trong thời gian Dash (0.2s)
        Vector2 dashDirection = moveInput.normalized == Vector2.zero ? Vector2.right : moveInput.normalized;

        playerRD.linearVelocity = dashDirection * _dashSpeed;

        yield return new WaitForSeconds(_dashDuration);

        // Nhả vận tốc về lại 0 sau khi dash xong để không bị trôi lướt quá đà
        playerRD.linearVelocity = Vector2.zero;
        isDashing = false;
    }
}