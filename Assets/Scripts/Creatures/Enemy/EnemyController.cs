using Components.ColliderBased;
using Components.Health;
using Creatures.AI;
using Creatures.Player;
using Sections;
using System.Linq;
using UnityEngine;

namespace Creatures.Enemy
{
    public class EnemyController : Creature
    {
        [SerializeField] protected int _spawnPointPrice;
        [SerializeField] protected EnemyState _initialState;
        [Space]
        [SerializeField] protected ColliderTrigger _vision;
        [SerializeField] protected AttackComponent[] _attacks;

        public enum EnemyState
        {
            Idle,
            Patrolling,
            Chasing,
            Attack
        }

        protected EnemyState _state;
        protected bool _seePlayer;
        protected AttackComponent _activeAttack;

        private const string PATROLLING_TRIGGER = "patrolling";
        private const string CHASING_TRIGGER = "chasing";

        public int SpawnPointPrice => _spawnPointPrice;

        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void Start()
        {
            StartInitialState();

            _vision.OnPlayerEnterTrigger += Vision_OnPlayerEnterTrigger;
            _vision.OnPlayerExitTrigger += Vision_OnPlayerExitTrigger;
        }

        public void StartInitialState()
        {
            if (_seePlayer)
            {
                SetState(EnemyState.Chasing);
                _animator.SetTrigger(CHASING_TRIGGER);
                return;
            }

            if (_initialState == EnemyState.Patrolling)
            {
                SetState(EnemyState.Patrolling);
                _animator.SetTrigger(PATROLLING_TRIGGER);
            }
            else
            {
                SetState(EnemyState.Idle);
            }
        }

        protected virtual void Vision_OnPlayerEnterTrigger(object sender, System.EventArgs e)
        {
            _seePlayer = true;
        }

        protected virtual void Vision_OnPlayerExitTrigger(object sender, System.EventArgs e)
        {
            _seePlayer = false;
        }

        protected override void Update()
        {
            base.Update();

            if ((_state == EnemyState.Idle || _state == EnemyState.Patrolling) && _seePlayer)
            {
                SetState(EnemyState.Chasing);
                _animator.SetTrigger(CHASING_TRIGGER);
                return;
            }

            if (_activeAttack == null)
            {
                ChooseAttack();
            }
        }

        protected virtual void ChooseAttack()
        {
            AttackComponent[] availableAttacks = _attacks.Where(attack => attack.CanAttack).ToArray();
            if (availableAttacks.Length > 0)
            {
                SetState(EnemyState.Attack);
                if (availableAttacks.Length == 1)
                {
                    _activeAttack = availableAttacks[0];
                    _animator.SetTrigger(availableAttacks[0].Attack());
                }
                if (availableAttacks.Length > 1)
                {
                    int randomAttack = Random.Range(0, availableAttacks.Length - 1);
                    _activeAttack = availableAttacks[randomAttack];
                    _animator.SetTrigger(availableAttacks[randomAttack].Attack());
                }
            }
        }

        protected void SetState(EnemyState state)
        {
            if (_state == state)
                return;

            _state = state;
            Debug.Log(_state.ToString());
        }

        public virtual void OnAttack()
        {
            foreach (HealthComponent health in _activeAttack.OnAttack())
            {
                if (health != PlayerController.Instance.GetComponent<HealthComponent>())
                    continue;

                health.ModifyHealth(-_activeAttack.Damage);
            }
            _activeAttack = null;
        }

        protected virtual void OnDestroy()
        {
            Section.Instance.ReduseSpawnedEnemiesCount();
        }
    }
}