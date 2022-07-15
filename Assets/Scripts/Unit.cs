using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;

    public float MaxHealth { get; private set; }
    public float Health { get; private set; }

    private void Start()
    {
        const float MaxHealthStartValue = 100;

        MaxHealth = MaxHealthStartValue;
        Health = MaxHealthStartValue;
        _healthBar.SetMaxValue(MaxHealth);
        _healthBar.SetValue(Health, true);
    }

    public void GetDamaged(float damageValue)
    {
        Health -= damageValue;

        if (Health < 0)
            Health = 0;

        ChangeHealthBarValues();
    }

    public void GetHealed()
    {
        const float HealValue = 10;

        Health += HealValue;

        if (Health > MaxHealth)
            Health = MaxHealth;

        ChangeHealthBarValues();
    }

    private void ChangeHealthBarValues()
    {
        _healthBar.SetValue(Health);
    }
}
