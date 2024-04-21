using System;
using System.Collections;
using System.Collections.Generic;
using Components.UI.Store;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private bool _haveProduct = true;
    public static Trader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void TryShowStore()
    {
        // if (!_haveProduct) return;
        StoreUI.Instance.Open();
    }

    public void Disable()
    {
        _haveProduct = false;
    }
}