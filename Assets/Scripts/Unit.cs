using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private UnityEvent _healthChanged;

    public float MaxHealth { get; private set; }
    public float Health { get; private set; }

    public event UnityAction OnHealthChanged
    {
        add => _healthChanged.AddListener(value);
        remove => _healthChanged.RemoveListener(value);
    }

    private void Start()
    {
        MaxHealth = _maxHealth;
        Health = _maxHealth;

        if (_healthChanged == null)
            _healthChanged = new UnityEvent();
    }

    public void GetDamaged(float damageValue)
    {
        Health -= damageValue;

        Health = Mathf.Clamp(Health, 0, MaxHealth);

        _healthChanged?.Invoke();
    }

    public void GetHealed(float healValue)
    {
        Health += healValue;

        Health = Mathf.Clamp(Health, 0, MaxHealth);

        _healthChanged?.Invoke();
    }

    public void ChangeMaxHealth(float maxHealthValue)
    {
        MaxHealth = maxHealthValue;
        _healthChanged?.Invoke();
    }
}
