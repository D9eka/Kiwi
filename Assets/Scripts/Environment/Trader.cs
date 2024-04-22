using Components.UI;
using Components.UI.Screens.Store;
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
        UIController.Instance.PushScreen(StoreUI.Instance);
    }

    public void Disable()
    {
        _haveProduct = false;
    }
}