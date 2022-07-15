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

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        UpdateMaxValue();
        UpdateValue(true);
    }

    public void UpdateValue(bool isFast = false)
    {
        if (isFast)
        {
            _slider.value = _unit.Health;
        }
        else
        {
            _targetValue = _unit.Health;
            CalculateSpeed();
            RestartCoroutine(_moving, MoveBarValue());
        }
    }

    public void UpdateMaxValue()
    {
        _slider.maxValue = _unit.MaxHealth;
    }

    private void CalculateSpeed()
    {
        const float Divisor = 10;

        float valueBetwhen = Mathf.Abs(_slider.value - _targetValue);

        _adaptiveSlideSpeed = valueBetwhen / Divisor * _slideSpeed;
    }

    private void RestartCoroutine(Coroutine coroutine, IEnumerator enumerator)
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
