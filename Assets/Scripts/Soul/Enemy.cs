using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _soulDrop = 5;
    [SerializeField] private GameObject _soulPrefab;
    private HealthManager _healthManager;

    private void Awake()
    {
        _healthManager = GetComponent<HealthManager>();
    }
    private void OnEnable()
    {
        _healthManager.OnDeath += Die;
    }

    private void OnDisable()
    {
        _healthManager.OnDeath -= Die;
    }

    private void Die()
    {
        int randomSoul = Random.Range(0, _soulDrop);

        for (int i = 0; i < randomSoul; i++) 
        {
            Vector3 offset = new Vector3(
                          Random.Range(-1f, 1f),
                          Random.Range(-1f, 1f),
                          0f
                      );
            Instantiate(_soulPrefab, transform.position + offset, Quaternion.identity);
        }
    }
}
