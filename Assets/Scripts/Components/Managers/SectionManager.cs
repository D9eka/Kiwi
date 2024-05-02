using Components.UI;
using Components.UI.Screens;
using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sections
{
    public class SectionManager : MonoBehaviour
    {
        [SerializeField] private List<SectionSO> _sections;
        [SerializeField] private SectionTypeSO _finalSectionType;
        [SerializeField] private SectionTypeSO _secretSectionType;

        private List<SectionSO> _openedSections = new();
        private SectionSO _secretSection;
        private bool _isInSecretSection;

        private Dictionary<SectionTypeSO, List<SectionSO>> _sectionDictionary = new();

        private bool _isLoading;

        private const int INITIAL_SPAWN_POINTS = 30;
        private const int MAX_ROOMS = 8;

        public enum SectionSpawnPosition
        {
            Start,
            End,
            Secret
        }

        public int OpenedSectionsCount => _openedSections.Count;
        public int CurrentSectionIndex { get; private set; }
        public bool NeedStartWave { get; private set; }

        public static SectionManager Instance { get; private set; }
        public int SecretDoorSectionIndex = -1;
        public SectionTypeSO CurrentSectionType => _openedSections[CurrentSectionIndex].SectionTypeSO;

        public EventHandler OnStartLoadingSection;
        public EventHandler OnFinishLoadingSection;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (_openedSections.Count == 0)
                _openedSections.Add(Section.Instance.Data);
            FillSectionsDictionary();
            Load();
            ChipRewardUI.Instance.OnPlayerChooseChip += ChipRewardUI_OnPlayerChooseChip;
        }

        private void FillSectionsDictionary()
        {
            foreach (var section in _sections)
            {
                SectionTypeSO sectionType = section.SectionTypeSO;
                if (!_sectionDictionary.ContainsKey(sectionType))
                    _sectionDictionary.Add(sectionType, new List<SectionSO>());
                _sectionDictionary[sectionType].Add(section);
            }
        }

        private void ChipRewardUI_OnPlayerChooseChip(object sender, EventArgs e)
        {
            MyGameManager.RoomPassed++;
            List<SectionTypeSO> _possibleSectionTypes = GetRandomSectionTypes();
            UIController.Instance.PushScreen(NextSectionSelectionUI.Instance);
            NextSectionSelectionUI.Instance.SetTypes(_possibleSectionTypes);
        }

        public int GetSpawnPoints(SectionTypeSO sectionSO)
        {
            return INITIAL_SPAWN_POINTS + Mathf.RoundToInt(CurrentSectionIndex * 1.8f) + sectionSO.SpawnPointBonus;
        }

        private void LoadSection(SectionSO sectionSO, SectionSpawnPosition position)
        {
            if (_isLoading)
                return;
            _isLoading = true;
            UIController.Instance.PushScreen(LoadingScreen.Instance);
            StartCoroutine(LoadSectionRoutine(sectionSO, position));
        }

        private IEnumerator LoadSectionRoutine(SectionSO sectionSO, SectionSpawnPosition position)
        {
            OnStartLoadingSection?.Invoke(this, EventArgs.Empty);
            NeedStartWave = position == SectionSpawnPosition.Start;
            Save();
            yield return new WaitForFixedUpdate();
            Vector2 playerPosition = position switch
            {
                SectionSpawnPosition.Start => sectionSO.StartPosition,
                SectionSpawnPosition.End => sectionSO.EndPosition,
                SectionSpawnPosition.Secret => sectionSO.SecretPosition,
                _ => throw new NotImplementedException()
            };
            new SceneManager().LoadScene(sectionSO.SectionName, playerPosition, position != SectionSpawnPosition.Start);
            _isLoading = false;
        }

        public void EnterSecretSection()
        {
            _isInSecretSection = true;
            _secretSection = _secretSection != null ? _secretSection :
                             Randomiser.GetRandomElement(_sectionDictionary.Where(data => data.Key.SectionType == SectionType.Secret)
                                                                           .Select(data => data.Value)
                                                                           .First());
            LoadSection(_secretSection, SectionSpawnPosition.Start);
        }

        public void EnterPreviousSection()
        {
            if (CurrentSectionIndex <= 1)
                return;
            if (_isInSecretSection)
            {
                _isInSecretSection = false;
                LoadSection(_openedSections[CurrentSectionIndex], SectionSpawnPosition.Secret);
            }
            else
            {
                CurrentSectionIndex -= 1;
                LoadSection(_openedSections[CurrentSectionIndex], SectionSpawnPosition.End);
            }
        }

        public void EnterNextSection()
        {
            if (CurrentSectionIndex < _openedSections.Count - 1 && _openedSections[CurrentSectionIndex + 1] != null)
            {
                CurrentSectionIndex += 1;
                LoadSection(_openedSections[CurrentSectionIndex], SectionSpawnPosition.Start);
            }
            else if (CurrentSectionIndex == 0)
            {
                EnterNextSection(_sectionDictionary.Keys.First(key => key.SectionType == SectionType.Base));
            }
            else
            {
                UIController.Instance.PushScreen(ChipRewardUI.Instance);
            }
        }

        public void EnterNextSection(SectionTypeSO sectionTypeSO)
        {
            List<SectionSO> availableSections = _sectionDictionary[sectionTypeSO].Where(sectionSO => !_openedSections.Contains(sectionSO)).ToList();
            if (availableSections.Count == 0)
                availableSections = _sectionDictionary[sectionTypeSO];
            _openedSections.Add(Randomiser.GetRandomElement(availableSections));
            CurrentSectionIndex += 1;
            LoadSection(_openedSections[CurrentSectionIndex], SectionSpawnPosition.Start);
        }

        public List<SectionTypeSO> GetRandomSectionTypes()
        {
            List<SectionTypeSO> finalSectionTypes = new List<SectionTypeSO>();
            List<SectionTypeSO> keys = _sectionDictionary.Keys.ToList();
            if (CurrentSectionIndex == MAX_ROOMS)
            {
                finalSectionTypes.Add(_finalSectionType);
                return finalSectionTypes;
            }
            if (CurrentSectionType.SectionType != SectionType.Base)
                keys.Remove(CurrentSectionType);
            keys.Remove(_secretSectionType);
            keys.Remove(_finalSectionType);
            int count = Mathf.Min(3, keys.Count);
            return Randomiser.GetRandomElements(keys, count);
        }

        [ContextMenu(nameof(ShowCurrentSection))]
        public void ShowCurrentSection()
        {
            Debug.Log(_openedSections[0]);
        }

        private void Save()
        {
            SectionManagerData.OpenedSections = _openedSections;
            SectionManagerData.CurrentSectionIndex = CurrentSectionIndex;
            SectionManagerData.IsInSecretSection = _isInSecretSection;
            SectionManagerData.NeedStartWave = NeedStartWave;
            SectionManagerData.SecretDoorSectionIndex = SecretDoorSectionIndex;
        }

        private void Load()
        {
            if (SectionManagerData.OpenedSections != null)
            {
                _openedSections = SectionManagerData.OpenedSections;
                CurrentSectionIndex = SectionManagerData.CurrentSectionIndex;
                _isInSecretSection = SectionManagerData.IsInSecretSection;
                NeedStartWave = SectionManagerData.NeedStartWave;
                SecretDoorSectionIndex = SectionManagerData.SecretDoorSectionIndex;
            }
        }
    }

    public static class SectionManagerData
    {
        public static List<SectionSO> OpenedSections;
        public static int CurrentSectionIndex;
        public static bool IsInSecretSection;
        public static bool NeedStartWave;
        public static int SecretDoorSectionIndex;
        /*
        public SectionManagerData(List<SectionSO> openedSections, int currentSectionIndex, bool needStartWave, int secretDoorSectionIndex)
        {
            OpenedSections = openedSections;
            CurrentSectionIndex = currentSectionIndex;
            NeedStartWave = needStartWave;
            SecretDoorSectionIndex = secretDoorSectionIndex;
        }*/

        public static void Clear()
        {
            OpenedSections = null;
            CurrentSectionIndex = 0;
            IsInSecretSection = false;
            NeedStartWave = false;
            SecretDoorSectionIndex = -1;
        }
    }
}