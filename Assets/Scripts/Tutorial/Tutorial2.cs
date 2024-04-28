
using UnityEngine;

public class Tutorial2 : MonoBehaviour
{
    [SerializeField] private Speaker _beforeBattleSpeaker;
    [SerializeField] private Speaker _afterPipeObtainSpeaker;


    private void Start()
    {
        _beforeBattleSpeaker.StartSpeak();
    }

    public void StartDialogAfterPipeObtain()
    {
        _afterPipeObtainSpeaker.StartSpeak();
    }
}