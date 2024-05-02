using System;
using UnityEngine;

namespace Player
{
    public class PlayerVisual : MonoBehaviour
    {
        public EventHandler OnFinishPuchAttackAnimation;

        public EventHandler OnStartAttackAnimation;
        public EventHandler OnAttackAnimation;
        public EventHandler OnFinishAttackAnimation;

        public EventHandler OnStartReloadAnimation;
        public EventHandler OnFinishReloadAnimation;

        public EventHandler OnStartDeathAnimation;
        public EventHandler OnFinishDeathAnimation;

        public void FinishPunchAttackAnimation()
        {
            OnFinishPuchAttackAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void StartAttackAnimation()
        {
            OnStartAttackAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void OnAttack()
        {
            OnAttackAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void FinishAttackAnimation()
        {
            OnFinishAttackAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void StartRealoadAnimation()
        {
            OnStartReloadAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void FinishReloadAnimation()
        {
            OnFinishReloadAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void StartDeathAnimation()
        {
            OnStartDeathAnimation?.Invoke(this, EventArgs.Empty);
        }

        public void FinishDeathAnimation()
        {
            OnFinishDeathAnimation?.Invoke(this, EventArgs.Empty);
        }
    }
}
