using UnityEngine;

public class PlayerAbsorb : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private float _absorbRange = 4f;

    private void Start()
    {
        PlayerInputReader.Instance.OnAbsorb += OnAbsorbPerformed;
    }

    private void OnDisable()
    {
        PlayerInputReader.Instance.OnAbsorb -= OnAbsorbPerformed;
    }


    private void OnAbsorbPerformed()
    {
        PullItems();
    }

    private void PullItems()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            _absorbRange,
            itemLayer
        );

        foreach (Collider2D hit in hits)
        {
            Soul soul = hit.GetComponent<Soul>();
            if (soul != null)
            {
                soul.SetTarget(transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _playerStats.AbsorbRange);
    }
}