using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components.UI;
using Creatures.Player;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private List<WindowUI> _openedWindows = new();
    private HUD _hud;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerInputReader.Instance.OnUIClose += OnPlayerUIClose;
        _hud = HUD.Instance;
    }

    private void OnPlayerUIClose(object sender, EventArgs e)
    {
        if (_openedWindows.Count <= 0) return;
        if (!_openedWindows[^1].IsClosable) return;
        TryCloseLastWindow();
    }

    public void OpenNewWindow(WindowUI window)
    {
        if (_openedWindows.Count >= 1) _openedWindows[^1].Hide();
        window.Show();
        _openedWindows.Add(window);
        ShowHUD(false);
    }

    public void TryCloseLastWindow()
    {
        if (_openedWindows.Count <= 0) return;
        var window = _openedWindows[^1];
        _openedWindows.Remove(window);
        window.Hide();
        if (_openedWindows.Count <= 0)
        {
            ShowHUD();
        }
        else _openedWindows[^1].Show();
    }

    public void CloseAllWindows()
    {
        while (_openedWindows.Count > 0)
        {
            TryCloseLastWindow();
        }
    }

    public void TryCloseUntilCertainWindow(GameObject gameObject)
    {
        var window = gameObject.GetComponent<WindowUI>();
        if (!_openedWindows.Contains(window)) return;
        while (_openedWindows[^1] != window)
        {
            TryCloseLastWindow();
        }
    }

    public void ShowHUD(bool value = true)
    {
        _hud.gameObject.SetActive(value);
    }
}