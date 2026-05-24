using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField] private AbyssFormData _abyssData;

    [SerializeField] private PlayerSoul _playerSoul;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Color _abyssColor = Color.blue;
    private Color _originColor;

    private AbyssFormModifier _abyssModifier;

    private void Awake()
    {
        _abyssModifier = new AbyssFormModifier(_abyssData);
        _originColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        _playerSoul.OnEnterAbyss += EnterAbyss;
        _playerSoul.OnExitAbyss += ExitAbyss;
    }

    private void OnDisable()
    {
        _playerSoul.OnEnterAbyss -= EnterAbyss;
        _playerSoul.OnExitAbyss -= ExitAbyss;
    }

    private void EnterAbyss()
    {
        _playerStats.AddModifier(_abyssModifier);

        _spriteRenderer.color = _abyssColor;
    }

    private void ExitAbyss()
    {
        _playerStats.RemoveModifier(_abyssModifier);

        _spriteRenderer.color = _originColor;
    }
}