using Creatures.AI;
using UnityEngine;

public class SpawnComponents : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _objectToSpawn;
    [Space]
    [SerializeField] private float _minItems;
    [SerializeField] private float _maxItems;
    [Space]
    [SerializeField] private float _initialPositionXRange = 1f;
    [SerializeField] private float _initialPositionYRange = 1.5f;
    [Space]
    [SerializeField] private float _initialVelocityXMin = 1f;
    [SerializeField] private float _initialVelocityXMax = 10f;
    [SerializeField] private float _initialVelocityYMin = 1f;
    [SerializeField] private float _initialVelocityYMax = 10f;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        int itemsCount = (int)Random.Range(_minItems, _maxItems);

        for (int i = 0; i < itemsCount; i++)
        {
            Vector2 initialPosition = new Vector2(Random.Range(_target.position.x - _initialPositionXRange, _target.position.x + _initialPositionXRange),
                                                  Random.Range(_target.position.y - _initialPositionYRange, _target.position.y + _initialPositionYRange));
            Vector2 initialVelocity = new Vector2(Random.Range(_initialVelocityXMin, _initialVelocityXMax), Random.Range(_initialVelocityYMin, _initialVelocityYMax));
            GameObject item = Instantiate(_objectToSpawn, initialPosition, Quaternion.identity);
            if (item.TryGetComponent(out AINavigation navigation))
                navigation.Initialize();
            item.GetComponent<Rigidbody2D>().velocity = initialVelocity;
        }
    }
}
