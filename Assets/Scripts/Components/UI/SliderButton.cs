using UnityEngine;
using UnityEngine.UI;

namespace Components.UI
{
    public class SliderButton : MonoBehaviour
    {
        [SerializeField] private int _changeValue;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponentInParent<Slider>();

            GetComponent<Button>().onClick.AddListener(() => ChangeValue());
        }

        public void ChangeValue()
        {
            _slider.value += _changeValue;
        }
    }
}