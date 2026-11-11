using UnityEngine;

public sealed class EnemyHealth : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth = 100;
    [SerializeField] private bool destroyOnDeath = true;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || CurrentHealth <= 0) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        if (CurrentHealth == 0)
        {
            if (destroyOnDeath)
                Destroy(gameObject);
        }
    }
}
