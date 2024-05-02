using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Components.UI.Screens.Dialogs
{
    public class DialogBoxController : ScreenComponent
    {
        [SerializeField] private float _textSpeed = 0.05f;
        [SerializeField] protected TextMeshProUGUI _nameText;
        [SerializeField] protected TextMeshProUGUI _phraseText;

        protected Sentence _currentSentence => _data.Sentences[_currentSentenceIndex];

        private DialogData _data;
        private int _currentSentenceIndex;
        private Coroutine _typingRoutine;
        private UnityEvent _onFinishDialog;

        public static DialogBoxController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void ShowDialog(DialogData data, UnityEvent onStart = null, UnityEvent onFinish = null)
        {
            if (_content.activeInHierarchy)
            {
                OnContinue();
                return;
            }

            UIController.Instance.PushScreen(this);
            onStart?.Invoke();
            _onFinishDialog = onFinish;

            _data = data;
            _currentSentenceIndex = -1;
            _phraseText.text = "";

            ShowDialog(_data, onStart, onFinish);
        }

        private IEnumerator TypeDialogText()
        {
            _phraseText.text = string.Empty;

            var text = _currentSentence.Phrase;
            foreach (var letter in text)
            {
                _phraseText.text += letter;
                yield return new WaitForSeconds(_textSpeed);
            }

            _typingRoutine = null;
        }

        public void OnSkip()
        {
            if (_typingRoutine == null)
                return;

            StopTypeAnimation();
            _phraseText.text = _data.Sentences[_currentSentenceIndex].Phrase;
        }

        public void OnContinue()
        {
            if (_typingRoutine != null)
            {
                OnSkip();
                return;
            }

            StopTypeAnimation();
            _currentSentenceIndex++;

            var isDialogComplete = _currentSentenceIndex >= _data.Sentences.Length;
            if (isDialogComplete)
            {
                StopDialog();
            }
            else
            {
                SetSpeakerName();
                _typingRoutine = StartCoroutine(TypeDialogText());
            }
        }

        private void StopDialog()
        {
            _onFinishDialog?.Invoke();
            UIController.Instance.PopScreen();
        }

        private void StopTypeAnimation()
        {
            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);
            _typingRoutine = null;
        }

        private void SetSpeakerName()
        {
            _nameText.text = _currentSentence.SpeakerName;
        }
    }
}