using System;
using UnityEngine.Events;

namespace Components.UI.ScreenEvent
{
    [Serializable]
    public class ScreenEventComponent
    {
        public string Label;
        public UnityEvent UnityEvent;
    }
}
