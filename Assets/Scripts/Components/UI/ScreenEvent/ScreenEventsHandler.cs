using System.Collections.Generic;
using UnityEngine;

namespace Components.UI.ScreenEvent
{
    public class ScreenEventsHandler : MonoBehaviour
    {
        [SerializeField] protected GameObject _screenEventPrefab;

        private List<ScreenEventUI> _spawnedEvents = new();

        protected const string CANCEL_EVENT_KEY = "ESC";
        protected const string ADDITIONAL_EVENT_KEY = "Tab";
        protected const string CONFIRM_EVENT_KEY = "Enter";

        public void FillEventButtons(ScreenEventComponent cancelEvent, ScreenEventComponent additionalEvent, ScreenEventComponent confirmEvent)
        {
            foreach (ScreenEventUI screenEvent in _spawnedEvents)
            {
                Destroy(screenEvent.gameObject);
            }
            _spawnedEvents = new();

            if (cancelEvent != null && cancelEvent.Label != "")
                CreateEventButton(CANCEL_EVENT_KEY, cancelEvent);
            if (additionalEvent != null && additionalEvent.Label != "")
                CreateEventButton(ADDITIONAL_EVENT_KEY, additionalEvent);
            if (confirmEvent != null && confirmEvent.Label != "")
                CreateEventButton(CONFIRM_EVENT_KEY, confirmEvent);
        }

        private void CreateEventButton(string eventKey, ScreenEventComponent screenEvent)
        {
            ScreenEventUI screenEventGO = Instantiate(_screenEventPrefab, transform).GetComponent<ScreenEventUI>();
            screenEventGO.Fill(eventKey, screenEvent.Label, screenEvent.UnityEvent);
            _spawnedEvents.Add(screenEventGO);
        }
    }
}
