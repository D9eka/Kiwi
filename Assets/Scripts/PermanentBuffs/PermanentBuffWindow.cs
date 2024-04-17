using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PermanentBuffs
{
    public class PermanentBuffWindow : MonoBehaviour
    {
        [SerializeField] PermanentBuff _activeGain;
        [Space]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private GameObject _essenceLeftHandler;
        [SerializeField] private TextMeshProUGUI _essenceLeft;
        [SerializeField] private Button _addEssenceButton;

        public static PermanentBuffWindow Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            SetGain(_activeGain);
        }

        public void SetGain(PermanentBuff gain)
        {
            _activeGain = gain;

            _name.text = gain.Data.Name;
            _description.text = gain.Data.Description;

            int essenceLeft = gain.Data.Price - gain.SpentEssence;
            if (essenceLeft <= 0)
            {
                _essenceLeftHandler.SetActive(false);
                _addEssenceButton.gameObject.SetActive(false);
            }
            else
            {
                _essenceLeftHandler.SetActive(true);
                _essenceLeft.text = essenceLeft.ToString();
                _addEssenceButton.gameObject.SetActive(true);
                _addEssenceButton.onClick.RemoveAllListeners();
                _addEssenceButton.onClick.AddListener(() => _activeGain.SpendEssence(1));
            }
        }
    }
}
