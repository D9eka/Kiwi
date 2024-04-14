using Components.Health;
using Creatures.Player;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI
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