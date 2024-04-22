using System;
using System.Collections;
using System.Collections.Generic;
using Sections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NextSectionSelectionUI : MonoBehaviour
{
    [SerializeField] private SectionBlockUI _leftChoiceBlock;
    [SerializeField] private SectionBlockUI _centerChoiceBlock;
    [SerializeField] private SectionBlockUI _rightChoiceBlock;
    private int _count;
    public static NextSectionSelectionUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowChoice()
    {
        gameObject.SetActive(true);
    }

    public void SetTypes(List<SectionTypeSO> sectionTypes)
    {
        _count = sectionTypes.Count;
        switch (_count)
        {
            case 1:
                Destroy(_leftChoiceBlock.gameObject);
                Destroy(_rightChoiceBlock.gameObject);
                _centerChoiceBlock.SetInfo(sectionTypes[0]);
                break;
            case 2:
                Destroy(_centerChoiceBlock.gameObject);
                _leftChoiceBlock.SetInfo(sectionTypes[0]);
                _rightChoiceBlock.SetInfo(sectionTypes[1]);
                break;
            default:
                _leftChoiceBlock.SetInfo(sectionTypes[0]);
                _centerChoiceBlock.SetInfo(sectionTypes[1]);
                _rightChoiceBlock.SetInfo(sectionTypes[2]);
                break;
        }
    }

    public void SetCertainSection(SectionSO sectionSO)
    {
        Destroy(_leftChoiceBlock.gameObject);
        Destroy(_rightChoiceBlock.gameObject);
        _centerChoiceBlock.SetInfo(sectionSO);
    }
}