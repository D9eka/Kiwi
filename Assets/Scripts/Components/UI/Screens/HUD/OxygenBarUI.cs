using Components.Oxygen;
using Creatures.Player;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBarUI : MonoBehaviour
{
    [SerializeField] private Image _filler;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private OxygenComponent _oxygen;

    private void Start()
    {
        _oxygen = PlayerController.Instance.GetComponent<OxygenComponent>();
        _oxygen.OnValueChange += OnOxygenValueChanged;
        _oxygen.ChangeOxygenStats(0);
    }

    private void OnOxygenValueChanged(object sender, OxygenComponent.OnValueChangeEventArgs e)
    {
        var oxygenPercent = e.value / e.maxValue;
        SetFillerValue(oxygenPercent);
        SetText(e.value);
    }


    private void SetFillerValue(float value)
    {
        _filler.fillAmount = value;
    }

    private void SetText(float value)
    {
        var a = TimeSpan.FromSeconds(value);
        _textMeshProUGUI.text = a.ToString("mm':'ss");
    }
}