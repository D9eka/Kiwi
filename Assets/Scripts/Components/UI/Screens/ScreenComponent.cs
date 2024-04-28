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
        [SerializeField] protected ScreenEventComponent _additionlEvent;
        [SerializeField] protected ScreenEventComponent _confirmEvent;
        [SerializeField] protected bool _haveEventButtons;
        [SerializeField] protected Transform _eventsHandler;
        [SerializeField] protected GameObject _screenEventPrefab;

        protected const string CANCEL_EVENT_KEY = "ESC";
        protected const string ADDITIONAL_EVENT_KEY = "Пробел";
        protected const string CONFIRM_EVENT_KEY = "Enter";

        public bool ExitOnNewPagePush => _exitOnNewPagePush;
        public bool DisablePlayerInput => _disablePlayerInput;

        public bool HasEvents => _haveEvents;

        protected virtual void Start()
        {
        }

        protected virtual void FillEventButtons()
        {
            /*
            if (_haveEventButtons)
            {
                Transform[] spawnedEvents = _eventsHandler.GetComponentsInChildren<Transform>();
                for (int i = 0; i < spawnedEvents.Length; i++)
                    Destroy(spawnedEvents[i].gameObject);

                if (_cancelEvent != null)
                    CreateEventButton(CANCEL_EVENT_KEY, _cancelEvent);
                if (_additionlEvent != null)
                    CreateEventButton(ADDITIONAL_EVENT_KEY, _additionlEvent);
                if (_confirmEvent != null)
                    CreateEventButton(CONFIRM_EVENT_KEY, _confirmEvent);
            }
            */
        }

        protected void CreateEventButton(string eventKey, ScreenEventComponent screenEvent)
        {
            GameObject screenEventGO = Instantiate(_screenEventPrefab, _eventsHandler);
            screenEventGO.GetComponent<ScreenEventUI>().Fill(eventKey, screenEvent.Label, screenEvent.UnityEvent);
        }

        public virtual void Enter()
        {
            _content.SetActive(true);
            FillEventButtons();

            if (_firstFocusItem != null)
            {
                EventSystem.current.SetSelectedGameObject(_firstFocusItem);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
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
                    _additionlEvent.UnityEvent?.Invoke();
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
