using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creatures.Enemy
{
    public class KamikazeEnemyController : EnemyController
    {
        protected override void Vision_OnPlayerEnterTrigger(object sender, System.EventArgs e)
        {
            _activeAttack = _attacks[0];
            _animator.SetTrigger(_attacks[0].Attack());
        }

        protected override void Update()
        {
            return;
        }

        public override void OnAttack()
        {
            base.OnAttack();

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            GetComponent<SpawnComponents>().Spawn();
        }
    }
}
