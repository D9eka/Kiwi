using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Components.UI.ScreenEvent
{
    public class ScreenEventUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _keyText;
        [SerializeField] private TextMeshProUGUI _labelText;

        private Button _button;

        public void Fill(string keyText, string labelText, UnityEvent unityEvent)
        {
            _button = GetComponent<Button>();
            _keyText.text = keyText;
            _labelText.text = labelText;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => unityEvent?.Invoke());
        }
    }
}
