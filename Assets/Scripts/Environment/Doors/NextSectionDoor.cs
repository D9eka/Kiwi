using System;
using System.Collections.Generic;
using Sections;
// using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class NextSectionDoor : Door
{
    //Сделал возможность показывать 1, 2 или 3 варианта
    [SerializeField] private bool _isRandom = true;
    [SerializeField, Range(1, 3)] private int _choicesCount = 2;
    [SerializeField] private List<SectionTypeSO> _possibleSectionTypes = new List<SectionTypeSO>();
    [SerializeField] private SectionSO _notRandomSection;

    private void Start()
    {
        if (CurrentSectionManager.Instance.IsCompleted)
        {
            Open();
            return;
        }

        CurrentSectionManager.Instance.OnSectionComplete += OnCurrentSectionComplete;
    }

    private void OnCurrentSectionComplete(object sender, EventArgs e)
    {
        Open();
    }

    private void SetChoice()
    {
        if (_isRandom)
        {
            _possibleSectionTypes = SectionManager.Instance.GetRandomSectionTypes(_choicesCount);
            NextSectionSelectionUI.Instance.SetTypes(_possibleSectionTypes);
        }
        else NextSectionSelectionUI.Instance.SetCertainSection(_notRandomSection);
    }

    private void ShowChoice()
    {
        NextSectionSelectionUI.Instance.ShowChoice();
    }

    protected override void Enter()
    {
        Debug.Log(SectionManager.Instance.CurrentSectionIndex);
        Debug.Log(SectionManager.Instance.OpenedSectionsCount);
        if (SectionManager.Instance.CurrentSectionIndex + 1 < SectionManager.Instance.OpenedSectionsCount)
        {
            SectionManager.Instance.EnterNextOpenedSection();
        }
        else
        {
            SetChoice();
            ShowChoice();
        }
    }
}