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
    [SerializeField] private TextMeshProUGUI _sectionTypeName;
    [SerializeField] private Image _image;
    private SectionTypeSO _sectionType;
    private SectionSO _section;

    public void SetInfo(SectionTypeSO sectionTypeSO)
    {
        _sectionType = sectionTypeSO;
        _sectionTypeName.text = sectionTypeSO.Name;
        _image.sprite = sectionTypeSO.Image;
    }
    public void SetInfo(SectionSO sectionSO)
    {
        _section = sectionSO;
        _sectionType = sectionSO.SectionTypeSO;
        _sectionTypeName.text =  _sectionType.Name;
        _image.sprite = _sectionType.Image;
    }

    public void MakeChoice()
    {
        SectionManager.Instance.EnterNextNewSection(_section);
    }
}