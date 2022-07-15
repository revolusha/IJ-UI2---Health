using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public UnityEvent HealthChanged;
    public UnityEvent MaxHealthChanged;
    public float MaxHealth { get; private set; }
    public float Health { get; private set; }

    private void Start()
    {
        MaxHealth = _maxHealth;
        Health = _maxHealth;

        if (HealthChanged == null)
            HealthChanged = new UnityEvent();

        if (MaxHealthChanged == null)
            MaxHealthChanged = new UnityEvent();
    }

    public void GetDamaged(float damageValue)
    {
        Health -= damageValue;

        Health = Mathf.Clamp(Health, 0, MaxHealth);

        HealthChanged.Invoke();
    }

    public void GetHealed(float healValue)
    {
        Health += healValue;

        Health = Mathf.Clamp(Health, 0, MaxHealth);

        HealthChanged.Invoke();
    }

    public void ChangeMaxHealth(float maxHealthValue)
    {
        MaxHealth = maxHealthValue;
        MaxHealthChanged.Invoke();
    }
}
