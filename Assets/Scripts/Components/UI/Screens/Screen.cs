using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Components.UI.Screens
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _firstFocusItem;
        [SerializeField] private GameObject _eventsHandler;
        [Space]
        [SerializeField] private bool _exitOnNewPagePush;
        [SerializeField] private bool _mustBeFirstWindow;
        [Space]
        [SerializeField] private ScreenEvent[] _events;
        //cancel, confirm, additional

        public bool ExitOnNewPagePush => _exitOnNewPagePush;
        public bool MustBeFirstWindow => _mustBeFirstWindow;

        public virtual void Enter()
        {
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
    }
}

[Serializable]
public class ScreenEvent
{
    public string Label;
    public string Key;
    public UnityEvent UnityEvent;
}
