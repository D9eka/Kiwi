using Components.UI;
using Components.UI.Screens;
using UnityEngine;

public class EngineerDesk : MonoBehaviour
{
    public void OpenChipUpgradeMenu()
    {
        UIController.Instance.PushScreen(UpdateChipUI.Instance);
    }
}