using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NavigationPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    public List<Transform> Points => _points;
}