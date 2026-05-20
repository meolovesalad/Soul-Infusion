using UnityEngine;
/*
    Manager sở hữu actions
    Manager tạo actions
    Manager quản lifecycle của actions

    => nên nó phải enable/disable.
*/
[DefaultExecutionOrder(-100)]
public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    public InputSystem_Actions Actions { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        Actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        Actions.Enable();
    }

    private void OnDisable()
    {
        Actions.Disable();
    }
}