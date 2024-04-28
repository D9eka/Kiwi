using Sections;
using System;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    [SerializeField] private Speaker _beforeBattleSpeaker;
    [SerializeField] private Speaker _afterBattleSpeaker;



    private void Start()
    {
        Section.Instance.OnEndSpawnWaves += Section_OnEndSpawnWaves;
        _beforeBattleSpeaker.StartSpeak();
    }

    private void Section_OnEndSpawnWaves(object sender, EventArgs e)
    {
        StartDialogAfterBattle();
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