using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsData _baseData;

    public float MoveSpeed { get; private set; }
    public float DashSpeed { get; private set; }
    public float CastRate { get; private set; }
    public float ProjectileDamage { get; private set; }
    public float AbsorbRange { get; private set; }
    public float DashCooldown { get; private set; }

    // nhét các  các modifier vo list
    private readonly List<IStatModifier> _modifiers = new();
    private void Awake()
    {
        RecalculateStats();
    }

    public void AddModifier(IStatModifier modifier)
    {
        _modifiers.Add(modifier);
        RecalculateStats();
    }

    public void RemoveModifier(IStatModifier modifier)
    {
        _modifiers.Remove(modifier);
        RecalculateStats();
    }

    private void RecalculateStats()
    {
        MoveSpeed = _baseData.moveSpeed;
        DashSpeed = _baseData.dashSpeed;
        CastRate = _baseData.castRate;
        ProjectileDamage = _baseData.projectileDamage;
        AbsorbRange = _baseData.absorbRange;
        DashCooldown = _baseData.dashCooldown;

        foreach (var modifier in _modifiers)
        {
            modifier.Apply(this);
        }
    }

    public void AddMoveSpeed(float value)
    {
        MoveSpeed += value;
    }

    public void AddDashSpeed(float value)
    {
        DashSpeed += value;
    }

    public void AddCastRate(float value)
    {
        CastRate += value;
    }

    public void AddDamage(float value)
    {
        ProjectileDamage += value;
    }

    public void AddAbsorbRange(float value)
    {
        AbsorbRange += value;
    }

    public void AddDashCooldown(float value)
    {
        DashCooldown -= value;
    }
}