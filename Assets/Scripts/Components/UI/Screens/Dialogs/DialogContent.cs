using TMPro;
using UnityEngine;

namespace Components.UI.Screens.Dialogs
{
    public class DialogContent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public TextMeshProUGUI Text => _text;
    }
}