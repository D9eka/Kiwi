using Components.Health;
using System.Linq;

namespace Creatures.Enemy
{
    public class BossEnemyController : EnemyController
    {
        private HealthComponent _health;

        private const string SPEED_UP_KEY = "speed-up";

        protected override void Awake()
        {
            base.Awake();
            _health = GetComponent<HealthComponent>();
        }

        protected override void Start()
        {
            base.Start();

            _health.OnValueChange += Health_OnValueChange;
        }

        private void Health_OnValueChange(object sender, HealthComponent.OnValueChangeEventArgs e)
        {
            _animator.SetBool(SPEED_UP_KEY, e.value < 58);
        }
    }
}