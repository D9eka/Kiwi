using System.Collections;
using System.Collections.Generic;
using Sections;
using UnityEngine;

public abstract class Door : MonoBehaviour
{
    public bool IsOpened { get; private set; }
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _openedDoorSprite;

    // [SerializeField] private DoorType _doorType;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Open()
    {
        IsOpened = true;
        // _spriteRenderer.sprite = _openedDoorSprite;
        _spriteRenderer.color = Color.white;
    }

    public void TryEnter()
    {
        if (!IsOpened) return;
        Enter();
    }

    protected abstract void Enter();
    // {
    //     if (IsOpened) return;
    //     switch (_doorType)
    //     {
    //         case DoorType.Previous:
    //             SectionManager.Instance.EnterPreviousOpenedSection();
    //             break;
    //         case DoorType.Next:
    //             if (SectionManager.Instance.CurrentSectionIndex + 1 < SectionManager.Instance.OpenedSectionsCount)
    //             {
    //                 SectionManager.Instance.EnterNextOpenedSection();
    //             }
    //             else
    //             {
    //                 SetChoice();
    //                 ShowChoice();
    //             }
    //             break;
    //     }
    // }
}