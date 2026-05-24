public class AbyssFormModifier : IStatModifier
{
    private readonly AbyssFormData _data;

    public AbyssFormModifier(AbyssFormData data)
    {
        _data = data;
    }

    public void Apply(PlayerStats stats)
    {
        stats.AddMoveSpeed(_data.bonusMoveSpeed);
        stats.AddDamage(_data.bonusDamage);
        stats.AddCastRate(_data.bonusCastRate);
    }
}