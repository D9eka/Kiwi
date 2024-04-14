using UnityEngine;
using UnityEngine.Events;

namespace Components.Interactables
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;
        [SerializeField] private GameObject _hintUI;

        public void Interact()
        {
            _action?.Invoke();
            Debug.Log(name + "interacted");
        }

        public void ShowHintUI(bool show = true)
        {
            _hintUI.SetActive(show);
        }
    }
}