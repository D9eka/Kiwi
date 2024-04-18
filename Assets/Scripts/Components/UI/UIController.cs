using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Screen = Components.UI.Screens.Screen;

namespace Components.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Screen _initialScreen;

        private Canvas _canvas;
        private Stack<Screen> _screenStack = new Stack<Screen>();

        public static UIController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            if(_initialScreen != null)
            {
                PushScreen(_initialScreen);
            }
        }

        private void OnCancel()
        {
            if (_canvas.enabled && _canvas.gameObject.activeInHierarchy)
            {
                if (_screenStack.Count != 0)
                {
                    PopScreen();
                }
            }
        }

        public bool IsScreenInStack(Screen screen) => _screenStack.Contains(screen);

        public bool IsScreenOnTopOfStack(Screen screen) => _screenStack.Count > 0 && screen == _screenStack.Peek();

        public void PushScreen(Screen screen)
        {
            screen.Enter();

            if (_screenStack.Count > 0) 
            { 
                Screen currentScreen = _screenStack.Peek();

                if(currentScreen.ExitOnNewPagePush)
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
                Screen screen = _screenStack.Pop();
                screen.Exit();

                Screen newCurrentScreen = _screenStack.Peek();
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
    }
}
