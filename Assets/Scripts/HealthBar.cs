using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _slideSpeed = 3;

    private Slider _slider;
    private float _targetValue;
    private float _adaptiveSlideSpeed;
    private bool _isSliding;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _isSliding = false;
    }

    private void Update()
    {
        if (_isSliding)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, Time.deltaTime * _adaptiveSlideSpeed);

            if (_slider.value == _targetValue)
                _isSliding = false;
        }
    }

    public void SetValue(float healthValue, bool isFast = false)
    {
        if (isFast)
        {
            _slider.value = healthValue;
        }
        else
        {
            _targetValue = healthValue;
            CalculateSpeed();
             _isSliding = true;
        }
    }

    public void SetMaxValue(float maxHealthValue)
    {
        _slider.maxValue = maxHealthValue;
    }

    private void CalculateSpeed()
    {
        const float Divisor = 10;

        float valueBetwhen = Mathf.Abs(_slider.value - _targetValue);

        _adaptiveSlideSpeed = valueBetwhen / Divisor * _slideSpeed;
    }
}
