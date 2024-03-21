using System.Collections;
using System.Collections.Generic;
using Sections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu]
public class SectionTypeSO : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] private SectionType _sectionType;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _image;

    public SectionType SectionType => _sectionType;
    public string Name => _name;
    public Sprite Image => _image;
}