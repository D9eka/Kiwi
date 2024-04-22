using System;
using System.Collections;
using System.Collections.Generic;
using Sections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SecretSectionDoorOut : Door
{
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
        SectionManager.Instance.ExitSecretSection();
    }
}