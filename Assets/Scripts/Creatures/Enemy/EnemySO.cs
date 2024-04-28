using UnityEngine;

[CreateAssetMenu]
public class EnemySO : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _cost;
    public GameObject EnemyPrefab => _enemyPrefab;
    public int Cost => _cost;
}