using System.Collections;
using System.Collections.Generic;
using Sections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SectionTypeSO : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] private SectionType sectionType;
    [SerializeField] private string name;
    [SerializeField] private Sprite image;

    public SectionType SectionType => sectionType;
    public string Name => name;
    public Sprite Image => image;
}