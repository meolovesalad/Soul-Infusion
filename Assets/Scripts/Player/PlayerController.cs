using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRD;
    private Vector3 originalScale;

    private bool isDashing;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _dashSpeed = 20f;
    [SerializeField] private float _dashDuration = 0.2f;

    private Vector2 MoveInput =>
        PlayerInputReader.Instance.MoveValue;

    private void Awake()
    {
        playerRD = GetComponent<Rigidbody2D>();
        originalScale = playerRD.transform.localScale;
    }


    private void Start()
    {
        PlayerInputReader.Instance.OnDash += Dash;
    }

    private void OnDisable()
    {
        PlayerInputReader.Instance.OnDash -= Dash;
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            MoveAround();
        }
    }

    private void Update()
    {
        Flip();
    }

    private void MoveAround()
    {
        playerRD.linearVelocity = MoveInput.normalized * _moveSpeed;
    }

    private void Dash()
    {
        if (!isDashing)
        {
            StartCoroutine(PlayerDash());
        }
    }

    private IEnumerator PlayerDash()
    {
        isDashing = true;

        // Lưu lại hướng di chuyển hiện tại lúc bấm nút để Dash thẳng theo hướng đó, 
        // Tránh việc đổi hướng đột ngột giữa chừng khi đang trong thời gian Dash (0.2s)
        Vector2 dashDirection =
            MoveInput == Vector2.zero
            ? Vector2.right
            : MoveInput.normalized;

        playerRD.linearVelocity = dashDirection * _dashSpeed;

        yield return new WaitForSeconds(_dashDuration);

        // Nhả vận tốc về lại 0 sau khi dash xong để không bị trôi lướt quá đà
        playerRD.linearVelocity = Vector2.zero;
        isDashing = false;
    }

    public void Flip()
    {
        if (MoveInput.x == 0) return;

        transform.localScale = new Vector3(
            MoveInput.x < 0
                ? -Mathf.Abs(originalScale.x)
                : Mathf.Abs(originalScale.x),
            originalScale.y,
            originalScale.z
        );
    }
}