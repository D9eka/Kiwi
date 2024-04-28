using Sections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SectionBlockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sectionTypeName;
    [SerializeField] private Image _image;
    private SectionTypeSO _sectionType;

    public void SetInfo(SectionSO sectionSO)
    {
        SetInfo(sectionSO.SectionTypeSO);
    }
    public void SetInfo(SectionTypeSO sectionTypeSO)
    {
        _sectionType = sectionTypeSO;
        _sectionTypeName.text = sectionTypeSO.Name;
        //_image.sprite = sectionTypeSO.Image;
    }

    public void MakeChoice()
    {
        SectionManager.Instance.EnterNextSection(_sectionType);
    }
}