using System;
using System.Collections;
using System.Collections.Generic;
using Sections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SecretDoor : MonoBehaviour
{
    public bool IsOpened { get; private set; }
    [SerializeField] private List<SectionTypeSO> _sectionTypeList;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _openedDoorSprite;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Open()
    {
        IsOpened = true;
        _spriteRenderer.sprite = _openedDoorSprite;
    }

    public void TryEnter()
    {
        if (!IsOpened) return;
        SectionManager.Instance.EnterRandomSectionType(_sectionTypeList);
    }
}