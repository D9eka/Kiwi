using System.Collections;
using System.Collections.Generic;
using Sections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SectionBlockUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI sectionTypeName;
    [SerializeField] private Image image;
    private SectionType sectionType;

    public void SetInfo(SectionTypeSO sectionTypeSO)
    {
        sectionType = sectionTypeSO.SectionType;
        sectionTypeName.text = sectionTypeSO.Name;
        image.sprite = sectionTypeSO.Image;
    }

    public void MakeChoice()
    {
        SectionManager.Instance.EnterRandomSection(sectionType);
    }
}