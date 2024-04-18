using UnityEngine;

namespace Components.UI.Screens
{
    public class HUD : MonoBehaviour
    {
        public static HUD Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}