using Components.UI.ScreenEvent;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Components.UI.UIController;

namespace Components.UI.Screens
{
    public class ScreenComponent : MonoBehaviour
    {
        [SerializeField] protected GameObject _content;
        [SerializeField] protected GameObject _firstFocusItem;
        [Space]
        [SerializeField] protected bool _exitOnNewPagePush;
        [SerializeField] protected bool _disablePlayerInput;
        [Space]
        [SerializeField] protected bool _haveEvents;
        [SerializeField] protected ScreenEventComponent _cancelEvent;
        [SerializeField] protected ScreenEventComponent _additionalEvent;
        [SerializeField] protected ScreenEventComponent _confirmEvent;
        [Space]
        [SerializeField] protected bool _haveEventButtons;
        [SerializeField] protected ScreenEventsHandler _eventsHandler;

        public bool ExitOnNewPagePush => _exitOnNewPagePush;
        public bool DisablePlayerInput => _disablePlayerInput;

        public bool HasEvents => _haveEvents;

        protected virtual void Start()
        {
            FillEventButtons();
        }

        protected virtual void FillEventButtons()
        {
            if (_haveEventButtons)
            {
                _eventsHandler.FillEventButtons(_cancelEvent, _additionalEvent, _confirmEvent);
            }
        }

        public virtual void Enter()
        {
            SoundManager.Instance?.PlaySound(SoundManager.Instance._clickGameSound);
            _content.SetActive(true);

            if (_firstFocusItem != null)
            {
                EventSystem.current.SetSelectedGameObject(_firstFocusItem);
            }
        }

        public virtual void Exit()
        {
            _content.SetActive(false);
        }

        public void InvokeUIEvent(UIEvent eventType)
        {
            switch (eventType)
            {
                case UIEvent.Cancel:
                    if (_cancelEvent.UnityEvent.GetPersistentEventCount() != 0 && _cancelEvent.UnityEvent.GetPersistentTarget(0) != null)
                        _cancelEvent.UnityEvent.Invoke();
                    else
                        Instance.PopScreen();
                    break;
                case UIEvent.Additional:
                    _additionalEvent.UnityEvent?.Invoke();
                    break;
                case UIEvent.Submit:
                    if (_cancelEvent.UnityEvent.GetPersistentEventCount() != 0 && _confirmEvent.UnityEvent.GetPersistentTarget(0) != null)
                        _confirmEvent.UnityEvent.Invoke();
                    else if (EventSystem.current.currentSelectedGameObject.TryGetComponent(out Button button))
                        button.onClick.Invoke();
                    break;
                default:
                    throw new NotImplementedException();

            }
        }
    }
}
