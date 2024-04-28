using Components.UI;
using Components.UI.Screens.Store;
using UnityEngine;

public class Trader : MonoBehaviour
{
    public static Trader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void TryShowStore()
    {
        UIController.Instance.PushScreen(StoreUI.Instance);
    }
}