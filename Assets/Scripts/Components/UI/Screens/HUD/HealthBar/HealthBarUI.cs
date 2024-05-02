using Components.Health;
using Creatures.Player;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _filler;
    [SerializeField] private bool _isPlayer;
    [SerializeField] private bool _isStatic;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private HealthComponent _healthComponent;

    private void Start()
    {
        if (_isPlayer)
            _healthComponent = PlayerController.Instance.GetComponent<HealthComponent>();
        else
            _healthComponent = GetComponentInParent<HealthComponent>();
        _healthComponent.OnValueChange += OnHealthComponentValueChange;
        _healthComponent.ChangeHealthStats(0);
    }

    private void LateUpdate()
    {
        if (_isStatic)
            return;
        transform.localScale = _healthComponent.transform.localScale;
    }

    private void OnHealthComponentValueChange(object sender, HealthComponent.OnValueChangeEventArgs e)
    {
        var healthPercent = e.value / e.maxValue;
        SetFillerValue(healthPercent);
        if (_textMeshProUGUI == null)
            return;
        SetText(e.value, e.maxValue);
    }


    private void SetFillerValue(float value)
    {
        _filler.fillAmount = value;
    }

    private void SetText(float health, float maxHealth)
    {
        _textMeshProUGUI.text = (int)Math.Ceiling(health) + "/" + (int)Math.Ceiling(maxHealth);
    }
}