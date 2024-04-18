using System;
using TMPro;
using UnityEngine;

public class EssenceUI : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    private GameManager _gameManager;

    void Start()
    {
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        _gameManager = GameManager.Instance;
        _gameManager.OnEssenceCountChanged += OnGameManagerEssenceCountChanged;
        _gameManager.GetEssence(0);
    }

    private void OnGameManagerEssenceCountChanged(object sender, EventArgs e)
    {
        SetText(GameManager.Instance.EssenceCount);
    }

    private void SetText(int count)
    {
        _textMeshProUGUI.text = count.ToString();
    }
}