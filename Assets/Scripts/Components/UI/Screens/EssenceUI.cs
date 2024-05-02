using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    void Start()
    {
        MyGameManager.OnEssenceCountChanged += OnGameManagerEssenceCountChanged;
        MyGameManager.GetEssence(0);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_textMeshProUGUI.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
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