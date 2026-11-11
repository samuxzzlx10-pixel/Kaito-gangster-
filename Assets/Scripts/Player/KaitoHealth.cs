using UnityEngine;

public sealed class KaitoHealth : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth = 100;
    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }
    public System.Action<int, int> HealthChanged;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        HealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || CurrentHealth <= 0) return;
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        HealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void Restore(int amount)
    {
        if (amount <= 0 || CurrentHealth <= 0) return;
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        HealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
}
