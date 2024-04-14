using Components.ColliderBased;
using Components.Health;
using Extensions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Weapons;

namespace Creatures.Player
{
    [RequireComponent(typeof(HealthComponent))]
    public class PlayerController : Creature
    {
        [Header("Checkers")] 
        [SerializeField] protected LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [Header("Jump")] 
        [SerializeField] protected float _jumpForce;

        [Header("Dash")]
        [SerializeField] protected int _dashCount;
        [SerializeField] private float _dashForce;
        [SerializeField] private float _dashDrug;
        [SerializeField] private float _dashCooldown;
        [SerializeField] private AttackComponent _dashAttack;

        [Header("Weapon")] 
        [SerializeField] private Weapon _activeWeapon;

        private HealthComponent _health;

        private float _initialGravity;
        private bool _isOnLadder;

        private int _dashCounter;
        private bool _canDashAttack;
        private bool _isDashing;

        private const string IS_ON_GROUND_KEY = "is-on-ground";

        public bool Active { get; private set; }

        public static PlayerController Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;

            _health = GetComponent<HealthComponent>();

            Active = true;
            _initialGravity = _rigidbody.gravityScale;
            _dashCounter = _dashCount;
        }

        private void Start()
        {
            PlayerInputReader inputReader = GetComponent<PlayerInputReader>();
            inputReader.OnMove += PlayerInputReader_OnMove;
            inputReader.OnJump += PlayerInputReader_OnJump;
            inputReader.OnDash += PlayerInputReader_OnDash;
            inputReader.OnInteract += PlayerInputReader_OnInteract;

            inputReader.OnAttack += PlayerInputReader_OnAttack;
            inputReader.OnWeaponReload += PlayerInputReader_OnWeaponReload;
        }

        #region Events

        private void PlayerInputReader_OnMove(object sender, Vector2 e)
        {
            SetDirection(e);
        }

        private void PlayerInputReader_OnJump(object sender, EventArgs e)
        {
            Jump();
        }

        private void PlayerInputReader_OnDash(object sender, EventArgs e)
        {
            if (_isDashing)
                return;
            StartCoroutine(Dash());
        }

        private void PlayerInputReader_OnInteract(object sender, EventArgs e)
        {
            _interactionCheck.Check();
        }

        private void PlayerInputReader_OnAttack(object sender, EventArgs e)
        {
            if (!_isOnLadder && _activeWeapon != null && _activeWeapon.gameObject.activeSelf)
                _activeWeapon.Attack();
        }

        private void PlayerInputReader_OnWeaponReload(object sender, EventArgs e)
        {
            if (_activeWeapon != null && _activeWeapon is Gun gun)
                gun.Reload();
        }

        #endregion

        protected override void Update()
        {
            if(_isDashing)
                return;

            _isGrounded = _groundCheck.IsTouchingLayer;

            Move();

            UpdateAnimations();
            UpdateSpriteDirection();
        }

        protected override void Move()
        {
            float xVelocity = _direction.x * _speed;
            float yVelocity = _isOnLadder ? _direction.y * _speed * GameManager.Instance.Gravity : _rigidbody.velocity.y;
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        }

        private void Jump()
        {
            if (_isGrounded || _isOnLadder)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, GameManager.Instance.Gravity * _jumpForce);
            }
        }

        private IEnumerator Dash()
        {
            if (_dashCounter > 0 && (_isGrounded || _isOnLadder))
            {
                float linearDrag = _rigidbody.drag;
                _rigidbody.drag = _dashDrug;
                _isDashing = true;
                _rigidbody.velocity = new Vector2(_dashForce * transform.localScale.x, 0f);
                _dashCounter--;
                yield return new WaitUntil(() => Math.Abs(_rigidbody.velocity.x) <= 0.5f);
                _rigidbody.drag = linearDrag;
                _isDashing = false;
                if (_canDashAttack)
                {
                    foreach (HealthComponent health in _dashAttack.OnAttack())
                    {
                        if (health == GetComponent<HealthComponent>())
                            continue;

                        health.ModifyHealth(-_dashAttack.Damage);
                    }
                }
                Invoke(nameof(ActivateDash), _dashCooldown);
            }
        }

        private void ActivateDash()
        {
            _dashCounter++;
        }

        public void Deactivate()
        {
            Active = false;
            GetComponent<PlayerInput>().DeactivateInput();
            _rigidbody.velocity = Vector2.zero;
        }

        public void SetLadderState(bool state)
        {
            _isOnLadder = state;
            _rigidbody.gravityScale = _isOnLadder ? 0 : _initialGravity * GameManager.Instance.Gravity;
        }

        public PlayerData SaveData()
        {
            var healthData = _health.SaveData();
            return new PlayerData(SceneManager.GetActiveScene().name, transform.position, transform.localScale.x,
                healthData.health, healthData.maxHealth, true);
        }

        public override void UpdateSpriteDirection()
        {
            float angleToMouse = transform.GetAngleToMouse();

            if (angleToMouse >= -90f && angleToMouse <= 90f)
                transform.localScale = new Vector2(1f, _visual.transform.localScale.y);
            else
                transform.localScale = new Vector2(-1f, _visual.transform.localScale.y);

            if (_activeWeapon != null && _activeWeapon is Gun gun)
                gun.UpdateSpriteDirection();
        }

        protected override void UpdateAnimations()
        {
            base.UpdateAnimations();
            _animator.SetBool(IS_ON_GROUND_KEY, _isGrounded);
        }

        public void SetActiveWeapon(Weapon weapon)
        {
            _activeWeapon = weapon;
        }
    }

    [System.Serializable]
    public struct PlayerData
    {
        public string Location { get; private set; }
        public Vector2? Position { get; private set; }
        public float Scale { get; private set; }

        public float Health { get; private set; }
        public float MaxHealth { get; private set; }

        public bool FirstStart { get; private set; }

        private const float DEFAULT_HEALTH = 100f;
        private const float DEFAULT_MAX_HEALTH = 100f;

        public PlayerData(string location, Vector2 position, float scale = 1f,
            float health = DEFAULT_HEALTH, float maxHealth = DEFAULT_MAX_HEALTH,
            bool firstStart = true)
        {
            Location = location;
            Position = position;
            Scale = scale;

            Health = health;
            MaxHealth = maxHealth;

            FirstStart = firstStart;
        }
    }
}