using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Sections
{
    [CreateAssetMenu]
    public class SectionSO : ScriptableObject
    {
        [SerializeField] private SectionTypeSO _sectionTypeSO;
        [SerializeField] private string _sectionName;

        public SectionTypeSO SectionTypeSO => _sectionTypeSO;
        public string SectionName => _sectionName;
        
    }
}