using Components.ColliderBased;
using Creatures.Player;
using System;
using UnityEngine;

namespace Environment
{
    public class Ladder : MonoBehaviour
    {
        private PlayerController _player;
        private ColliderTrigger _trigger;

        private void Start()
        {
            _player = PlayerController.Instance;

            _trigger = GetComponent<ColliderTrigger>();
            _trigger.OnPlayerEnterTrigger += Trigger_OnPlayerEnterTrigger;
            _trigger.OnPlayerExitTrigger += Trigger_OnPlayerExitTrigger;
            _trigger.OnEnemyEnterTrigger += Trigger_OnEnemyEnterTrigger;
            _trigger.OnEnemyExitTrigger += Trigger_OnEnemyExitTrigger;
        }

        private void Trigger_OnPlayerEnterTrigger(object sender, EventArgs e)
        {
            _player.SetLadderState(true);
        }

        private void Trigger_OnPlayerExitTrigger(object sender, EventArgs e)
        {
            _player.SetLadderState(false);
        }

        private void Trigger_OnEnemyEnterTrigger(object sender, Creatures.Enemy.EnemyController e)
        {
            e.SetLadderState(true);
        }

        private void Trigger_OnEnemyExitTrigger(object sender, Creatures.Enemy.EnemyController e)
        {
            e.SetLadderState(false);
        }
    }
}
