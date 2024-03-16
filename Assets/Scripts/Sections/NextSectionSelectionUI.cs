using System;
using System.Collections;
using System.Collections.Generic;
using Sections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextSectionSelectionUI : MonoBehaviour
{
    [SerializeField] private SectionBlockUI leftChoiceBlock;
    [SerializeField] private SectionBlockUI centerChoiceBlock;
    [SerializeField] private SectionBlockUI rightChoiceBlock;
    private int count;
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
        // leftChoiceBlock.gameObject.SetActive(true);
        // centerChoiceBlock.gameObject.SetActive(true);
        // rightChoiceBlock.gameObject.SetActive(true);
    }

    public void SetTypes(List<SectionTypeSO> sectionTypes)
    {
        count = sectionTypes.Count;
        switch (count)
        {
            case 1:
                Destroy(leftChoiceBlock.gameObject);
                Destroy(rightChoiceBlock.gameObject);
                centerChoiceBlock.SetInfo(sectionTypes[0]);
                break;
            case 2:
                Destroy(centerChoiceBlock.gameObject);
                leftChoiceBlock.SetInfo(sectionTypes[0]);
                rightChoiceBlock.SetInfo(sectionTypes[1]);
                break;
            default:
                leftChoiceBlock.SetInfo(sectionTypes[0]);
                centerChoiceBlock.SetInfo(sectionTypes[1]);
                rightChoiceBlock.SetInfo(sectionTypes[2]);
                break;
        }
    }
}