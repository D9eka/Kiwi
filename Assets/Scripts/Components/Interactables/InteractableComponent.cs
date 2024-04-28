using UnityEngine;
using UnityEngine.Events;

namespace Components.Interactables
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;
        [SerializeField] private GameObject _hintUI;
        [SerializeField] private bool _canBeInteracted = true;

        public void TryInteract()
        {
            if (!_canBeInteracted)
                return;
            _action?.Invoke();
            Debug.Log(name + "interacted");
        }

        public void TryShowHintUI(bool show = true)
        {
            if (!_canBeInteracted) return;
            _hintUI.SetActive(show);
        }

        public void Activate(bool isActivated = true)
        {
            _canBeInteracted = isActivated;
        }

        public void Deactivate()
        {
            _canBeInteracted = false;
        }
    }
}