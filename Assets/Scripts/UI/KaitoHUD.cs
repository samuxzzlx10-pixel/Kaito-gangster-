using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class KaitoHUD : MonoBehaviour
{
    [SerializeField] private KaitoHealth health;
    [SerializeField] private KaitoCombat combat;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text healthLabel;
    [SerializeField] private TMP_Text ammoLabel;

    private void OnEnable()
    {
        if (health != null) health.HealthChanged += UpdateHealth;
        if (combat != null) combat.AmmoChanged += UpdateAmmo;
        Refresh();
    }

    private void OnDisable()
    {
        if (health != null) health.HealthChanged -= UpdateHealth;
        if (combat != null) combat.AmmoChanged -= UpdateAmmo;
    }

    private void Refresh()
    {
        if (health != null) UpdateHealth(health.CurrentHealth, health.MaxHealth);
        if (combat != null) UpdateAmmo(combat.Ammo, combat.MagazineSize);
    }

    private void UpdateHealth(int current, int max)
    {
        if (healthBar != null)
        {
            healthBar.maxValue = max;
            healthBar.value = current;
        }
        if (healthLabel != null) healthLabel.text = $"VIDA  {current}/{max}";
    }

    private void UpdateAmmo(int current, int capacity)
    {
        if (ammoLabel != null) ammoLabel.text = $"MUNIÇÃO  {current}/{capacity}";
    }
}
