using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Creatures.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        public EventHandler<Vector2> OnPlayerMove;
        public EventHandler<bool> OnPlayerJump;
        public EventHandler OnPlayerInteract;


        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            OnPlayerMove?.Invoke(this, direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
                OnPlayerInteract?.Invoke(this, EventArgs.Empty);
        }
    }
}