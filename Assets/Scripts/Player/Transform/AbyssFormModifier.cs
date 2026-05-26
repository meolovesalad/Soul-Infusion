public class AbyssFormModifier : IStatModifier
{
    private readonly AbyssFormData _data;
    private int _stage = 1;

    public AbyssFormModifier(AbyssFormData data)
    {
        _data = data;
    }

    public void SetStage(int stage)
    {
        _stage = stage;
    }


    public void Apply(PlayerStats stats)
    {
        stats.AddMoveSpeed(_data.bonusMoveSpeed);
        stats.AddDamage(_data.bonusDamage);
        stats.AddCastRate(_data.bonusCastRate);
    }
}