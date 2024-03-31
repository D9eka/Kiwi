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

    public void Spawn()
    {
        int itemsCount = (int)Random.Range(_minItems, _maxItems);

        for(int i = 0; i < itemsCount; i++)
        {
            Vector2 initialVelocity = new Vector2(Random.Range(1, 10), Random.Range(1, 10));
            GameObject item = Instantiate(_objectToSpawn, _target.position, Quaternion.identity);
            item.GetComponent<Rigidbody2D>().velocity = initialVelocity;
        }
    }    
}
