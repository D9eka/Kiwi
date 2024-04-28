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
        }

        private void Trigger_OnPlayerEnterTrigger(object sender, EventArgs e)
        {
            _player.SetLadderState(true);
        }

        private void Trigger_OnPlayerExitTrigger(object sender, EventArgs e)
        {
            _player.SetLadderState(false);
        }
    }
}
