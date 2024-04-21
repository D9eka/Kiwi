using System;
using System.Collections;
using System.Collections.Generic;
using Components.UI.Dialogs;
using UnityEngine;
using UnityEngine.Serialization;

public class Tutorial1 : MonoBehaviour
{
    [SerializeField] private Speaker _beforeBattleSpeaker;
    [SerializeField] private Speaker _afterBattleSpeaker;

    private void OnBattleOver(object sender, EventArgs e)
    {
        StartDialogAfterBattle();
    }


    private void Start()
    {
        CurrentSectionManager.Instance.OnBattleOver += OnBattleOver;
        _beforeBattleSpeaker.StartSpeak();
    }

    public void StartDialog(Speaker speaker)
    {
        speaker.StartSpeak();
    }

    public void StartDialogAfterBattle()
    {
        _afterBattleSpeaker.StartSpeak();
    }
}