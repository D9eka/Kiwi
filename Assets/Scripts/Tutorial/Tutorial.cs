using Components.UI.Screens.Dialogs;
using Sections;
using System;
using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private DialogDef _startDialog;
    [SerializeField] private DialogDef _afterBattleDialog;



    private void Start()
    {
        if (_startDialog != null)
            StartCoroutine(InvokeStartSpeaker());
        if (_afterBattleDialog != null)
            SectionTutorial.Instance.OnEndSpawnWaves += SectionTutorial_OnEndSpawnWaves;
    }

    private IEnumerator InvokeStartSpeaker()
    {
        yield return new WaitForSeconds(1f);
        DialogBoxController.Instance.ShowDialog(_startDialog.Data);
    }

    private void SectionTutorial_OnEndSpawnWaves(object sender, EventArgs e)
    {
        DialogBoxController.Instance.ShowDialog(_afterBattleDialog.Data);
    }
}