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
        [SerializeField] private List<SectionSO> battleSections = new();
        [SerializeField] private List<SectionSO> traderSections = new();
        [SerializeField] private List<SectionSO> restSections = new();
        [SerializeField] private List<SectionSO> bossSections = new();
        [SerializeField] private SectionSO secretSection;
        [SerializeField] private SectionSO startSection;

        [SerializeField] private List<SectionTypeSO> possibleRandomSectionTypes =
            new();

        private List<SectionSO> openedSections = new();
        public int OpenedSectionsCount => openedSections.Count;
        private int currentSectionIndex;
        public int CurrentSectionIndex => currentSectionIndex;
        private Dictionary<SectionType, List<SectionSO>> sectionsDictionary = new();
        public static SectionManager Instance { get; private set; }
        private bool wasSecretSectionOpened;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            openedSections.Add(startSection);
            FillSectionsDictionary();
        }

        private void FillSectionsDictionary()
        {
            sectionsDictionary.Add(SectionType.Battle, battleSections);
            sectionsDictionary.Add(SectionType.Trader, traderSections);
            sectionsDictionary.Add(SectionType.Rest, restSections);
            sectionsDictionary.Add(SectionType.Boss, bossSections);
        }

        public void EnterSection(SectionSO sectionSO)
        {
            currentSectionIndex += 1;
            SceneManager.LoadScene(sectionSO.SectionName);
            openedSections.Add(sectionSO);
        }

        public void EnterRandomSection(SectionType sectionType)
        {
            //Не очень понял, что из себя представляет секрет, поэтому сделал так, что с определёной вероятностью появляется секретная комната
            var randomNumber = Random.Range(0, 10);
            if (randomNumber <= 2 && !wasSecretSectionOpened)
            {
                EnterSection(secretSection);
                wasSecretSectionOpened = true;
            }
            else
            {
                EnterSection(sectionsDictionary[sectionType][Random.Range(0, sectionsDictionary[sectionType].Count)]);
            }
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

            // Выглядит ужасно, но поскольку это вероятно не понадобится, пока что не заморачивался
            var finalSectionTypes = new List<SectionTypeSO>();
            var sectionTypeFirstIndex = Random.Range(0, possibleRandomSectionTypes.Count);
            var sectionTypeFirst = possibleRandomSectionTypes[sectionTypeFirstIndex];
            finalSectionTypes.Add(sectionTypeFirst);
            if (count == 1) return finalSectionTypes;
            var newPossibleRandomSectionTypes =
                possibleRandomSectionTypes.Take(sectionTypeFirstIndex)
                    .Concat(possibleRandomSectionTypes.Skip(sectionTypeFirstIndex + 1)).ToList();
            var sectionTypeSecondIndex = Random.Range(0, newPossibleRandomSectionTypes.Count);
            var sectionTypeSecond = newPossibleRandomSectionTypes[sectionTypeSecondIndex];
            finalSectionTypes.Add(sectionTypeSecond);
            if (count == 2) return finalSectionTypes;
            newPossibleRandomSectionTypes =
                newPossibleRandomSectionTypes.Take(sectionTypeSecondIndex)
                    .Concat(newPossibleRandomSectionTypes.Skip(sectionTypeSecondIndex + 1)).ToList();
            var sectionTypeThirdIndex = Random.Range(0, newPossibleRandomSectionTypes.Count);
            var sectionTypeThird = newPossibleRandomSectionTypes[sectionTypeThirdIndex];
            finalSectionTypes.Add(sectionTypeThird);
            return finalSectionTypes;
        }

        public void EnterNextOpenedSection()
        {
            currentSectionIndex += 1;
            SceneManager.LoadScene(openedSections[currentSectionIndex].SectionName);
        }

        public void EnterPreviousOpenedSection()
        {
            currentSectionIndex -= 1;
            SceneManager.LoadScene(openedSections[currentSectionIndex].SectionName);
        }
    }
}