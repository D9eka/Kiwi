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

    public void SetInfo(SectionTypeSO sectionTypeSO)
    {
        _sectionType = sectionTypeSO;
        _sectionTypeName.text = sectionTypeSO.Name;
        _image.sprite = sectionTypeSO.Image;
    }

    public void MakeChoice()
    {
        SectionManager.Instance.EnterRandomSection(_sectionType);
    }
}