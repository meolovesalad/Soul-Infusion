using UnityEngine;

public class TransformStatHandler : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private AbyssFormData _abyssData;

    private AbyssFormModifier _modifier;

    private void Awake()
    {
        _modifier = new AbyssFormModifier(_abyssData);
    }

    public void ApplyStats(int stage)
    {
        _modifier.SetStage(stage);

        _playerStats.AddModifier(_modifier);
    }

    public void RemoveStats()
    {
        _playerStats.RemoveModifier(_modifier);
    }
}