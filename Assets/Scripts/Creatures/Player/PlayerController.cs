using Components.ColliderBased;
using Components.Health;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Creatures.Player
{
    [RequireComponent(typeof(HealthComponent))]
    public class PlayerController : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [Header("Jump")]
        [SerializeField] private float _maxJumpTime;

        private HealthComponent health;

        private float jumpTimeCounter;

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
            inputReader.OnPlayerMove += PlayerController_OnPlayerMove;
            inputReader.OnPlayerInteract += PlayerController_OnPlayerInteract;
        }

        #region Events
        private void PlayerController_OnPlayerMove(object sender, Vector2 e)
        {
            SetDirection(e);
        }

        private void PlayerController_OnPlayerInteract(object sender, EventArgs e)
        {
            _interactionCheck.Check();
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

        protected override void Jump()
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
    }

    [System.Serializable]
    public struct PlayerData
    {
        public string Location { get; private set; }
        public Vector2? Position { get; private set; }
        public float Scale { get; private set; }

        public float Mana { get; private set; }
        public float MaxMana { get; private set; }

        public bool FirstStart { get; private set; }

        private const float DEFAULT_HEALTH = 100f;
        private const float DEFAULT_MAX_HEALTH = 100f;

        public PlayerData(string location, Vector2 position, float scale = 1f, 
                          float mana = DEFAULT_HEALTH, float maxMana = DEFAULT_MAX_HEALTH,
                          bool firstStart = true)
        {
            Location = location;
            Position = position;
            Scale = scale;

            Mana = mana;
            MaxMana = maxMana;

            FirstStart = firstStart;
        }
    }
}