using UnityEngine;

namespace Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _healthDelta;

        public void ModifyHealth(GameObject target)
        {
            if(target.transform.parent.TryGetComponent<HealthComponent>(out var healthComponent))
                healthComponent.ModifyHealth(_healthDelta);
        }
    }
}