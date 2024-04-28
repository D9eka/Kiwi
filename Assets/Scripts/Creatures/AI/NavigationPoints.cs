using System.Collections.Generic;
using UnityEngine;

public class NavigationPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    public List<Transform> Points => _points;
}