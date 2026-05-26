using UnityEngine;

public class TransformVisualController : MonoBehaviour
{
    /*
    Sau này:
        shader dissolve
        eye glow
        aura
        animation
    */
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Color _abyssColor = Color.blue;

    private Color _originColor;

    private void Awake()
    {
        _originColor = _spriteRenderer.color;
    }

    public void EnterVisual()
    {
        _spriteRenderer.color = _abyssColor;
    }

    public void ExitVisual() 
    {
        _spriteRenderer.color = _originColor;
    }
}
