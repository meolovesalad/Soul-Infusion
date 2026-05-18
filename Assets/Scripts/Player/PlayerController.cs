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

    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _dashSpeed = 20;
    [SerializeField] private float _dashDuration = 20;


    private void Awake()
    {
        playerRD = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Dash.performed += OnDashformed;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Dash.performed -= OnDashformed;
    }

    private void OnDashformed(InputAction.CallbackContext ctx)
    {
        StartCoroutine(PlayerDash());
    }

    void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (isDashing == false) { MoveAround(); }
    }

    private void MoveAround()
    {
        playerRD.linearVelocity = moveInput.normalized * _moveSpeed;
    }

    private IEnumerator PlayerDash()
    {
        isDashing = true;
        playerRD.linearVelocity = moveInput.normalized * _dashSpeed;
        yield return new WaitForSeconds(_dashDuration);
        isDashing = false;
    }
}
