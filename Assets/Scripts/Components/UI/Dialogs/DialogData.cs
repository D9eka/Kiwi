using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.UI.Dialogs
{
    [Serializable]
    public class DialogData
    {
        [SerializeField] private Sentence[] _sentences;
        public Sentence[] Sentences => _sentences;
    }

    [Serializable]
    public struct Sentence
    {
        [SerializeField] private string _speakerName;
        [SerializeField] private string _phrase;
        // [SerializeField] private AudioClip _voice;

        public string SpeakerName => _speakerName;
        public string Phrase => _phrase;
        // public AudioClip Voice => _voice;
    }
}