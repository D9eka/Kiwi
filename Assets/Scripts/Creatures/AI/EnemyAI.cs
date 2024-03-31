using System;
using System.Collections;
using Assets.Scripts.Creatures.AI;
using Components.ColliderBased;
using Components.Health;
using Creatures.Player;
using UnityEngine;

namespace Creatures.Enemy
{
    [RequireComponent(typeof(AINavigation))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;
        [Space]
        [SerializeField] private State _initialState = State.Patrolling;
        
        [Header("State: Patrolling")]
        [SerializeField] private float _maxPatrolDistance = 10f;
        [SerializeField] private float _alarmDelay = 0.5f;
        
        [Header("State: GoToPlayer")]
        [SerializeField] private float _missPlayerDelay = 5f;
        [SerializeField] private float _missPlayerCoolDown = 0.5f;
        
        [Header("State: Attack(Player)")]
        [SerializeField] private float _attackCoolDown = 1f;
        
        private Coroutine current;
        private float missPlayerCounter;

        private EnemyController enemy;
        private AINavigation navigation;
        private enum State
        {
            DoNothing,
            Patrolling,
            AgroToPlayer,
            GoToPlayer,
            AttackPlayer,
        }

        private State _state;

        public EventHandler<float> OnChangeSpeed;

        private void Start()
        {
            enemy = GetComponent<EnemyController>();
            navigation = GetComponent<AINavigation>();

            if (_initialState == State.Patrolling)
                StartState(State.Patrolling);
            else
                StartState(State.DoNothing);
        }

        private void EnemyController_OnDie(object sender, EventArgs e)
        {
            navigation.followEnabled = false;
        }

        public void OnPlayerInVision(GameObject player)
        {
            if (_state == State.GoToPlayer || _state == State.AttackPlayer)
                return;
            navigation.SetTarget(player);
            StartState(State.AgroToPlayer);
        }

        private IEnumerator DoNothing()
        {
            while (enabled)
                yield return null;
        }

        private IEnumerator AgroToPlayer()
        {
            navigation.followEnabled = false;
            yield return new WaitForSeconds(_alarmDelay);
            if (_vision.IsTouchingLayer)
                StartState(State.GoToPlayer);
            else
                StartState(_initialState);
        }

        private IEnumerator GoToPlayer()
        {
            missPlayerCounter = _missPlayerDelay;
            while (_vision.IsTouchingLayer || missPlayerCounter > 0)
            {
                if (!_vision.IsTouchingLayer)
                    missPlayerCounter -= Time.deltaTime;
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(State.AttackPlayer);
                }
                yield return null;
            }
            enemy.SetDirection(Vector2.zero);
            yield return new WaitForSeconds(_missPlayerCoolDown);
            StartState(_initialState);
        }

        private IEnumerator AttackPlayer()
        {
            navigation.followEnabled = false;
            while (_canAttack.IsTouchingLayer)
            {
                enemy.Attack();
                yield return new WaitForSeconds(_attackCoolDown);
            }
            StartState(State.GoToPlayer);
        }

        private void StartState(State state)
        {
            _state = state;
            navigation.followEnabled = true;
            Debug.Log($"{name} change state to {state}");
            enemy.Stop();

            if (current != null)
                StopCoroutine(current);

            if (state == State.Patrolling)
                current = StartCoroutine(navigation.DoPatrol());
            else
                current = StartCoroutine(state.ToString());
        }
    }
}