using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Creatures.Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        public EventHandler<Vector2> OnMove;
        public EventHandler OnJump;
        public EventHandler OnInteract;
        public EventHandler OnDash;

        public EventHandler OnAttack;
        public EventHandler OnWeaponReload;
        public EventHandler OnUIClose;
        public EventHandler OnSwitchWeapon;
        public EventHandler OnSpacebarPressed;
        public EventHandler OnEnterPressed;
        public EventHandler OnOpenInventory;
        public static PlayerInputReader Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }


        public void OnPlayerMove(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            OnMove?.Invoke(this, direction);
        }

        public void OnPlayerJump(InputAction.CallbackContext context)
        {
            OnJump?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerDash(InputAction.CallbackContext context)
        {
            OnDash?.Invoke(this, EventArgs.Empty);
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

        public void OnPlayerUIClose(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnUIClose?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerSwitchWeapon(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSwitchWeapon?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerSpacebarPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSpacebarPressed?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerEnterPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnEnterPressed?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerOpenInventory(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnOpenInventory?.Invoke(this, EventArgs.Empty);
        }
    }
}