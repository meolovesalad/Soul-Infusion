using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour, IDamageable
{
    //public IStats stats;

    //[SerializeField] private Image hpBar;
    public float currentHp;
    public float maxHp = 100f;
    //public UnityEvent OnDeath;
    public event Action<float, float> OnHealthChange;
    public event Action OnDeath;


    private void Start()
    {
        //hpBar.fillAmount = 1;
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        // sự kiện
        OnHealthChange?.Invoke(currentHp, maxHp);
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        if (currentHp <= maxHp)
        {
            currentHp += healAmount;
            currentHp = Mathf.Clamp(currentHp, 0, maxHp);

            //Debug.Log("hp had recovered");
            // sự kiện
            OnHealthChange?.Invoke(currentHp, maxHp);
        }
    }

    private void Die()
    {
        // Hoặc để OnDeath xử lý (ví dụ: chạy animation chết)
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

}

