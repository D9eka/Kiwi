using UnityEngine;

namespace Components.UI.Screens
{
    public class HUD : ScreenComponent
    {
        public static HUD Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}