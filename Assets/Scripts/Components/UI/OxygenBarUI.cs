using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OxygenBarUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image _filler;
    private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnOxygenValueChanged += OnGameManagerOxygenValueChanged;
    }

    private void OnGameManagerOxygenValueChanged(float oxygen, float maxOxygen)
    {
        var oxygenPercent = oxygen / maxOxygen;
        SetFillerValue(oxygenPercent);
        SetText(oxygen);
    }


    private void SetFillerValue(float value)
    {
        _filler.fillAmount = value;
    }

    private void SetText(float health)
    {
        var a = TimeSpan.FromSeconds(health);
        _textMeshProUGUI.text = a.ToString("mm':'ss");
    }
}