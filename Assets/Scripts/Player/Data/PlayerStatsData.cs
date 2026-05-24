using UnityEngine;

[CreateAssetMenu]
public class PlayerStatsData : ScriptableObject
{
    public float moveSpeed;
    public float dashSpeed;
    public float dashCooldown;
    //public float dashDuration;
    public float castRate;
    public float absorbRange;
    public float projectileDamage;
}
