using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Creatures.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        public EventHandler<Vector2> OnMove;
        public EventHandler<bool> OnJump;
        public EventHandler OnInteract;

        public EventHandler OnAttack;
        public EventHandler OnWeaponReload;


        public void OnPlayerMove(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            OnMove?.Invoke(this, direction);
        }

        public void OnPlayerInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
                OnInteract?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerAttack(InputAction.CallbackContext context)
        {
            OnAttack?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerWeaponReload(InputAction.CallbackContext context)
        {
            OnWeaponReload?.Invoke(this, EventArgs.Empty);
        }
    }
}