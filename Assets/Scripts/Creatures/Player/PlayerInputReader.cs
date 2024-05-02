using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static WeaponController;

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
        public EventHandler<WeaponPosition> OnSwitchWeapon;
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
            if (context.performed)
                OnJump?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDash?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteract?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttack?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerWeaponReload(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnWeaponReload?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerUIClose(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnUIClose?.Invoke(this, EventArgs.Empty);
        }

        public void OnPlayerSelectFirstWeapon(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSwitchWeapon?.Invoke(this, WeaponPosition.Weapon1);
        }

        public void OnPlayerSelectSecondWeapon(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSwitchWeapon?.Invoke(this, WeaponPosition.Weapon2);
        }

        public void OnPlayerSelectTrap(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSwitchWeapon?.Invoke(this, WeaponPosition.Trap);
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