using Creatures.Player;
using System;
using UnityEngine;

namespace Components.ColliderBased
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderTrigger : MonoBehaviour
    {
        public event EventHandler OnPlayerEnterTrigger;
        public event EventHandler OnPlayerExitTrigger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!collision.isTrigger && collision.GetComponentInParent<PlayerController>() != null)
                OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.isTrigger && collision.GetComponentInParent<PlayerController>() != null)
                OnPlayerExitTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
