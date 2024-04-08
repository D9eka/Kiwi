using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponents : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _objectToSpawn;
    [Space]
    [SerializeField] private float _minItems;
    [SerializeField] private float _maxItems;
    [Space]
    [SerializeField] private float _initialVelocityXMin = 1f;
    [SerializeField] private float _initialVelocityXMax = 10f;
    [SerializeField] private float _initialVelocityYMin = 1f;
    [SerializeField] private float _initialVelocityYMax = 10f;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        int itemsCount = (int)Random.Range(_minItems, _maxItems);

        for(int i = 0; i < itemsCount; i++)
        {
            Vector2 initialVelocity = new Vector2(Random.Range(_initialVelocityXMin, _initialVelocityXMax), Random.Range(_initialVelocityYMin, _initialVelocityYMax));
            GameObject item = Instantiate(_objectToSpawn, _target.position, Quaternion.identity);
            item.GetComponent<Rigidbody2D>().velocity = initialVelocity;
        }
    }    
}
