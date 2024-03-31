using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Creatures.Enemy
{
    public class KamikazeAnimations : MonoBehaviour
    {
        private Animator _animator;

        private const string WAKE_UP_KEY = "wake-up";
        private const string ATTACK_KEY = "attack";

        public EventHandler OnWakeUpEnd;
        public EventHandler OnAttackEnd;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void WakeUp()
        {
            _animator.SetTrigger(WAKE_UP_KEY);
        }

        public void OnWakeUpAnimEnd()
        {
            OnWakeUpEnd?.Invoke(this, EventArgs.Empty);
        }

        public void Attack()
        {
            _animator.SetTrigger(ATTACK_KEY);
        }

        public void OnAttackAnimEnd()
        {
            OnAttackEnd?.Invoke(this, EventArgs.Empty);
        }
    }
}
