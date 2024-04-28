using Creatures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerVisual : MonoBehaviour
    {
        public EventHandler OnFinishPuchAttackAnimation;

        public EventHandler OnStartAttackAnimation;
        public EventHandler OnFinishAttackAnimation;

        public EventHandler OnStartReloadAnimation;
        public EventHandler OnFinishRealoadAnimation;

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
            OnFinishRealoadAnimation?.Invoke(this, EventArgs.Empty);
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
