using Creatures.Player;
using Sections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using ScreenComponent = Components.UI.Screens.ScreenComponent;

namespace Components.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private ScreenComponent _initialScreen;

        private Canvas _canvas;

        private Stack<ScreenComponent> _screenStack = new Stack<ScreenComponent>();

        public enum UIEvent
        {
            Cancel,
            Additional,
            Submit
        }

        public static UIController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = Camera.main;
        }

        private void Start()
        {
            if (_initialScreen != null)
            {
                PushScreen(_initialScreen);
            }
        }

        public bool IsScreenInStack(ScreenComponent screen) => _screenStack.Contains(screen);

        public bool IsScreenOnTopOfStack(ScreenComponent screen) => _screenStack.Count > 0 && screen == _screenStack.Peek();

        public void PushScreen(ScreenComponent screen)
        {
            if (screen.DisablePlayerInput)
            {
                PlayerController.Instance?.GetComponent<PlayerInput>().DeactivateInput();
            }
            screen.Enter();

            if (_screenStack.Count > 0)
            {
                ScreenComponent currentScreen = _screenStack.Peek();

                if (currentScreen.ExitOnNewPagePush)
                {
                    currentScreen.Exit();
                }
            }
            _screenStack.Push(screen);
        }

        public void PopScreen()
        {
            if (_screenStack.Count > 1)
            {
                ScreenComponent screen = _screenStack.Pop();
                screen.Exit();
                if (screen.DisablePlayerInput)
                {
                    PlayerController.Instance?.GetComponent<PlayerInput>().ActivateInput();
                }

                ScreenComponent newCurrentScreen = _screenStack.Peek();
                if (newCurrentScreen.ExitOnNewPagePush)
                {
                    newCurrentScreen.Enter();
                }
            }
        }

        public void PopAllScreens()
        {
            for (int i = 1; i < _screenStack.Count; i++)
            {
                PopScreen();
            }
        }

        private void OnCancel()
        {
            SendUIEvent(UIEvent.Cancel);
        }

        private void OnAdditional()
        {
            SendUIEvent(UIEvent.Additional);
        }

        private void OnSubmit()
        {
            SendUIEvent(UIEvent.Submit);
        }

        private void SendUIEvent(UIEvent eventType)
        {
            if (_canvas.enabled && _canvas.gameObject.activeInHierarchy && _screenStack.Count != 0)
            {
                _screenStack.Peek().InvokeUIEvent(eventType);
            }
        }
    }
}
