using System;
using TMPro;
using UnityEngine;

public class EssenceUI : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;

    void Start()
    {
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        MyGameManager.OnEssenceCountChanged += OnGameManagerEssenceCountChanged;
        MyGameManager.GetEssence(0);
    }

    private void OnGameManagerEssenceCountChanged(object sender, EventArgs e)
    {
        SetText(MyGameManager.EssenceCount);
    }

    private void SetText(int count)
    {
        _textMeshProUGUI.text = count.ToString();
    }
}