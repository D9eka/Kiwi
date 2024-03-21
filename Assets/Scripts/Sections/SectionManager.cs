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
        [SerializeField] private SectionSO _secretSection;
        [SerializeField] private SectionSO _startSection;

        // [SerializeField] private List<SectionTypeSO> _possibleRandomSectionTypes = new();

        private List<SectionSO> _openedSections = new();
        public int OpenedSectionsCount => _openedSections.Count;
        public int CurrentSectionIndex { get; private set; }

        private Dictionary<SectionTypeSO, List<SectionSO>> _sectionsDictionary = new();
        public static SectionManager Instance { get; private set; }
        private bool _wasSecretSectionOpened;
        private SectionTypeSO CurrentSectionType => _openedSections[CurrentSectionIndex].SectionTypeSO;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _openedSections.Add(_startSection);
            FillSectionsDictionary();
        }

        private void FillSectionsDictionary()
        {
            foreach (var section in _sections)
            {
                var sectionType = section.SectionTypeSO;

                if (!_sectionsDictionary.ContainsKey(sectionType))
                    _sectionsDictionary.Add(sectionType, new List<SectionSO>());

                _sectionsDictionary[sectionType].Add(section);
            }
        }

        public void EnterSection(SectionSO sectionSO)
        {
            CurrentSectionIndex += 1;
            SceneManager.LoadScene(sectionSO.SectionName);
            _openedSections.Add(sectionSO);
        }

        public void EnterRandomSection(SectionTypeSO sectionType)
        {
            //Не очень понял, что из себя представляет секрет, поэтому сделал так, что с определёной вероятностью появляется секретная комната
            var randomNumber = Random.Range(0, 10);
            if (randomNumber <= 2 && !_wasSecretSectionOpened)
            {
                EnterSection(_secretSection);
                _wasSecretSectionOpened = true;
            }
            else
            {
                EnterSection(_sectionsDictionary[sectionType][Random.Range(0, _sectionsDictionary[sectionType].Count)]);
            }
        }

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
            var keys = _sectionsDictionary.Keys.ToList();
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
    }
}