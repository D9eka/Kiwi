using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Interactables
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;
        [SerializeField] private GameObject _hintUI;
        [SerializeField] private CurrentSectionManager _currentSectionManager;
        [SerializeField] private bool _canBeInteracted;

        private void Awake()
        {
            if (_currentSectionManager is not null)
                _currentSectionManager.OnBattleOver += OnBattleOver;
        }

        private void Start()
        {
            if (_currentSectionManager is null)
            {
                _currentSectionManager ??= CurrentSectionManager.Instance;
                _currentSectionManager.OnBattleOver += OnBattleOver;
            }
        }

        private void OnBattleOver(object sender, EventArgs e)
        {
            Activate();
        }

        public void TryInteract()
        {
            if (!_canBeInteracted) return;
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
    }
}