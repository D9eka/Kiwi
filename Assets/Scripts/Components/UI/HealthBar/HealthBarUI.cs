using System;
using System.Collections;
using System.Collections.Generic;
using Components.Health;
using Creatures.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image _filler;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private bool _isStatic;
    [SerializeField] private bool _isPlayer;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        if (_isPlayer) _healthComponent = PlayerController.Instance.GetComponent<HealthComponent>();
        _healthComponent ??= GetComponentInParent<HealthComponent>();
        _healthComponent.OnValueChange += OnHealthComponentValueChange;
        _healthComponent.ChangeHealthStats(0);
    }

    private void LateUpdate()
    {
        if (_isStatic) return;
        transform.localScale = _healthComponent.transform.localScale;
    }

    private void OnHealthComponentValueChange(object sender, HealthComponent.OnValueChangeEventArgs e)
    {
        var healthPercent = e.value / e.maxValue;
        SetFillerValue(healthPercent);
        if (_textMeshProUGUI is null) return;
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