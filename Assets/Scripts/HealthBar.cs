using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _slideSpeed = 3;
    [SerializeField] private Unit _unit;

    private Coroutine _moving;
    private Slider _slider;
    private float _targetValue;
    private float _adaptiveSlideSpeed;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _unit.OnHealthChanged += UpdateValue;
    }

    private void Start()
    {
        UpdateValueFast();
    }

    private void OnDisable()
    {
        _unit.OnHealthChanged -= UpdateValue;
    }

    private void UpdateValueFast()
    {
        _slider.maxValue = _unit.MaxHealth;
        _slider.value = _unit.Health;
    }

    private void UpdateValue()
    {
        _targetValue = _unit.Health;
        CalculateSpeed();
        RestartCoroutine(ref _moving, MoveBarValue());
    }

    private void CalculateSpeed()
    {
        const float Divisor = 10;

        float valueBetwhen = Mathf.Abs(_slider.value - _targetValue);

        _adaptiveSlideSpeed = valueBetwhen / Divisor * _slideSpeed;
    }

    private void RestartCoroutine(ref Coroutine coroutine, IEnumerator enumerator)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(enumerator);
    }

    private IEnumerator MoveBarValue()
    {
        while (_slider.value != _targetValue)
        {
            yield return new WaitForEndOfFrame();
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, Time.deltaTime * _adaptiveSlideSpeed);
        }
    }
}
