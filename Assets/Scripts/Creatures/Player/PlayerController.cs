using Components.ColliderBased;
using Components.Health;
using Extensions;
using System;
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
        [SerializeField] private float _maxJumpTime;

        [Header("Weapon")]
        [SerializeField] private Weapon _activeWeapon;

        private HealthComponent health;

        private float jumpTimeCounter;

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

            health = GetComponent<HealthComponent>();
            LoadData(PlayerPrefsController.GetPlayerData());

            Active = true;
        }

        private void LoadData(PlayerData data)
        {
            transform.position = data.Position.Value;
            transform.localScale = new Vector2(data.Scale, transform.localScale.y);
        }

        private void Start()
        {
            PlayerInputReader inputReader = GetComponent<PlayerInputReader>();
            inputReader.OnMove += PlayerInputReader_OnMove;
            inputReader.OnInteract += PlayerInputReader_OnInteract;

            inputReader.OnAttack += PlayerInputReader_OnAttack;
            inputReader.OnWeaponReload += PlayerInputReader_OnWeaponReload;
        }

        #region Events
        private void PlayerInputReader_OnMove(object sender, Vector2 e)
        {
            SetDirection(e);
        }

        private void PlayerInputReader_OnInteract(object sender, EventArgs e)
        {
            _interactionCheck.Check();
        }

        private void PlayerInputReader_OnAttack(object sender, EventArgs e)
        {
            if (_activeWeapon != null && _activeWeapon.gameObject.activeSelf)
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
            isGrounded = _groundCheck.IsTouchingLayer;

            Move();
            Jump();

            UpdateAnimations();
            UpdateSpriteDirection();
        }

        protected override void Move()
        {
            var xVelocity = direction.x * _speed;
            _rigidbody.velocity = new Vector2(xVelocity, _rigidbody.velocity.y);
        }

        private void Jump()
        {
            bool isJumpKeyPressed = direction.y > 0;

            if (isGrounded)
            {
                jumpTimeCounter = _maxJumpTime;

                if (isJumpKeyPressed)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
                }
                return;
            }
            if (!isGrounded && isJumpKeyPressed && jumpTimeCounter > 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                return;
            }
        }

        public void Deactivate()
        {
            Active = false;
            GetComponent<PlayerInput>().DeactivateInput();
            _rigidbody.velocity = Vector2.zero;
        }

        public PlayerData SaveData()
        {
            var healthData = health.SaveData();
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
            _animator.SetBool(IS_ON_GROUND_KEY, isGrounded);
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