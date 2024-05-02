using Creatures.Enemy;
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

        public event EventHandler<EnemyController> OnEnemyEnterTrigger;
        public event EventHandler<EnemyController> OnEnemyExitTrigger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.isTrigger)
            {
                if (collision.GetComponentInParent<PlayerController>() != null)
                    OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
                if (collision.transform.parent != null && collision.transform.parent.TryGetComponent(out EnemyController enemy))
                    OnEnemyEnterTrigger?.Invoke(this, enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.isTrigger)
            {
                if (collision.GetComponentInParent<PlayerController>() != null)
                    OnPlayerExitTrigger?.Invoke(this, EventArgs.Empty);
                if (collision.transform.parent != null && collision.transform.parent.TryGetComponent(out EnemyController enemy))
                    OnEnemyExitTrigger?.Invoke(this, enemy);
            }
        }
    }
}
