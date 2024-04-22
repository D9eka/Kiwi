using UnityEngine;

namespace Components.UI.Screens.Dialogs
{
    [CreateAssetMenu(fileName = "Dialog")]
    public class DialogDef : ScriptableObject
    {
        [SerializeField] private DialogData _data;
        public DialogData Data => _data;
    }
}