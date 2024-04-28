using UnityEngine;

namespace Sections
{
    [CreateAssetMenu]
    public class SectionSO : ScriptableObject
    {
        [SerializeField] private string _sectionName;
        [SerializeField] private SectionTypeSO _sectionTypeSO;
        [SerializeField] private bool _containsSecret;
        [Space]
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _endPosition;
        [SerializeField] private Vector2 _secretPosition;

        public string SectionName => _sectionName;
        public SectionTypeSO SectionTypeSO => _sectionTypeSO;
        public bool ContainsSecret => _containsSecret;

        public Vector2 StartPosition => _startPosition;
        public Vector2 EndPosition => _endPosition;
        public Vector2 SecretPosition => _secretPosition;
    }
}