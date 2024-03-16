using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sections
{
    [CreateAssetMenu]
    public class SectionSO : ScriptableObject
    {
        [SerializeField] private SectionTypeSO sectionTypeSO;
        [SerializeField] private string sectionName;

        public SectionTypeSO SectionTypeSO => sectionTypeSO;
        public string SectionName => sectionName;
        
    }
}