using System;
using System.Collections.Generic;
using Sections;
// using NaughtyAttributes;
using UnityEngine;

public class PreviousSectionDoor : Door
{
    private bool _isOpened;
    private void Start()
    {
        if (CurrentSectionManager.Instance.IsCompleted)
        {
            Open();
            return;
        }
        CurrentSectionManager.Instance.OnSectionComplete += OnCurrentSectionComplete;
    }

    private void OnCurrentSectionComplete(object sender, EventArgs e)
    {
        Open();
    }

    protected override void Enter()
    {
        SectionManager.Instance.EnterPreviousOpenedSection();
    }
}