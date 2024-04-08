using System;
using System.Collections.Generic;
using Sections;
// using NaughtyAttributes;
using UnityEngine;

public class PreviousSectionDoor : Door
{
    private bool _isOpened;

    protected override void Enter()
    {
        SectionManager.Instance.EnterPreviousOpenedSection();
    }
}