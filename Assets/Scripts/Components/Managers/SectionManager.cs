using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Sections
{
    public class SectionManager : MonoBehaviour
    {
        [SerializeField] private List<SectionSO> _sections = new();

        // [SerializeField] private SectionSO _secretSection;
        [SerializeField] private SectionSO _startSection;


        private List<SectionSO> _openedSections = new();
        private SectionSO _secretSection;
        public int OpenedSectionsCount => _openedSections.Count;
        public int CurrentSectionIndex { get; private set; }

        private Dictionary<SectionTypeSO, List<SectionSO>> _usualSectionDictionary = new();
        private List<SectionSO> _possibleSecretSections = new();
        public static SectionManager Instance { get; private set; }
        public bool _wasSecretSectionFound;
        public SectionTypeSO CurrentSectionType => _openedSections[CurrentSectionIndex].SectionTypeSO;


        private void Awake()
        {
            Instance = this;
            _openedSections.Add(_startSection);
        }

        private void Start()
        {
            FillSectionsDictionary();
        }

        private void FillSectionsDictionary()
        {
            foreach (var section in _sections)
            {
                var sectionType = section.SectionTypeSO;

                if (!_usualSectionDictionary.ContainsKey(sectionType))
                    _usualSectionDictionary.Add(sectionType, new List<SectionSO>());

                _usualSectionDictionary[sectionType].Add(section);
            }
        }

        public void EnterSecretSection()
        {
            _secretSection ??= Randomiser.GetRandomElement(_possibleSecretSections);
            SceneManager.LoadScene(_secretSection.SectionName);
        }

        public void ExitSecretSection()
        {
            SceneManager.LoadScene(_openedSections[CurrentSectionIndex].SectionName);
        }

        public void EnterNextNewSection(SectionSO sectionSO)
        {
            CurrentSectionIndex += 1;
            _openedSections.Add(sectionSO);
            SceneManager.LoadScene(sectionSO.SectionName);
        }

        public void EnterNextNewRandomSection(SectionTypeSO sectionType)
        {
            if (_wasSecretSectionFound)
                EnterNextNewSection(Randomiser.GetRandomNotSecretSection(_usualSectionDictionary[sectionType]));
            EnterNextNewSection(Randomiser.GetRandomElement(_usualSectionDictionary[sectionType]));
        }

        // public void EnterNextNewRandomSectionType(List<SectionTypeSO> sectionTypeList)
        // {
        //     EnterNextNewRandomSection(Randomiser.GetRandomElement(sectionTypeList));
        // }

        public void EnterNextOpenedSection()
        {
            CurrentSectionIndex += 1;
            SceneManager.LoadScene(_openedSections[CurrentSectionIndex].SectionName);
        }

        public void EnterPreviousOpenedSection()
        {
            CurrentSectionIndex -= 1;
            SceneManager.LoadScene(_openedSections[CurrentSectionIndex].SectionName);
        }

        public List<SectionTypeSO> GetRandomSectionTypes(int count)
        {
            if (count < 1)
            {
                Debug.LogError("Count less than 1");
                return new List<SectionTypeSO>();
            }

            if (count > 3)
            {
                Debug.LogError("Count more than 3 ");
                return new List<SectionTypeSO>();
            }

            var finalSectionTypes = new List<SectionTypeSO>();
            var keys = _usualSectionDictionary.Keys.ToList();
            keys.Remove(CurrentSectionType);
            while (finalSectionTypes.Count < count)
            {
                var randomIndex = Random.Range(0, keys.Count);
                var sectionType = keys[randomIndex];
                finalSectionTypes.Add(sectionType);
                keys.Remove(keys[randomIndex]);
            }

            return finalSectionTypes;
        }

        // public List<SectionTypeSO> GetRandomSectionTypesOld(int count)
        // {
        //     if (count < 1)
        //     {
        //         Debug.LogError("Count less than 1");
        //         return new List<SectionTypeSO>();
        //     }
        //
        //     if (count > 3)
        //     {
        //         Debug.LogError("Count more than 3 ");
        //         return new List<SectionTypeSO>();
        //     }
        //
        //     // Выглядит ужасно, но поскольку это вероятно не понадобится, пока что не заморачивался
        //     var finalSectionTypes = new List<SectionTypeSO>();
        //     var sectionTypeFirstIndex = Random.Range(0, _possibleRandomSectionTypes.Count);
        //     var sectionTypeFirst = _possibleRandomSectionTypes[sectionTypeFirstIndex];
        //     finalSectionTypes.Add(sectionTypeFirst);
        //     if (count == 1) return finalSectionTypes;
        //     var newPossibleRandomSectionTypes =
        //         _possibleRandomSectionTypes.Take(sectionTypeFirstIndex)
        //             .Concat(_possibleRandomSectionTypes.Skip(sectionTypeFirstIndex + 1)).ToList();
        //     var sectionTypeSecondIndex = Random.Range(0, newPossibleRandomSectionTypes.Count);
        //     var sectionTypeSecond = newPossibleRandomSectionTypes[sectionTypeSecondIndex];
        //     finalSectionTypes.Add(sectionTypeSecond);
        //     if (count == 2) return finalSectionTypes;
        //     newPossibleRandomSectionTypes =
        //         newPossibleRandomSectionTypes.Take(sectionTypeSecondIndex)
        //             .Concat(newPossibleRandomSectionTypes.Skip(sectionTypeSecondIndex + 1)).ToList();
        //     var sectionTypeThirdIndex = Random.Range(0, newPossibleRandomSectionTypes.Count);
        //     var sectionTypeThird = newPossibleRandomSectionTypes[sectionTypeThirdIndex];
        //     finalSectionTypes.Add(sectionTypeThird);
        //     return finalSectionTypes;
        // }
        [ContextMenu(nameof(ShowCurrentSection))]
        public void ShowCurrentSection()
        {
            Debug.Log(_openedSections[0]);
        }
    }
}