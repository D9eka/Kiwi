using Components.UI.Screens.Dialogs;
using UnityEngine;
using UnityEngine.Events;

public class Speaker : MonoBehaviour
{
    [SerializeField] private DialogDef _dialog;
    [SerializeField] private UnityEvent _onStartDialogEvent;
    [SerializeField] private UnityEvent _onFinishDialogEvent;

    public void StartSpeak()
    {
        DialogBoxController.Instance.ShowDialog(_dialog.Data, _onStartDialogEvent, _onFinishDialogEvent);
    }
}