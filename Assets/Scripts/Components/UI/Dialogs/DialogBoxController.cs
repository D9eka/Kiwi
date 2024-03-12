using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Components.UI.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;
        [Space]
        [SerializeField] private float _textSpeed = 0.05f;
        [Space]
        [SerializeField] protected DialogContent _content;

        protected Sentence CurrentSentence => data.Sentences[currentSentence];
        protected virtual DialogContent CurrentContent => _content;

        private const string IS_OPEN_KEY = "is-open";

        private DialogData data;
        private int currentSentence;
        private Coroutine typingRoutine;

        private UnityEvent onFinishDialog;

        public void ShowDialog(DialogData data, UnityEvent onStart, UnityEvent onFinish)
        {
            if (_container.activeSelf)
            {
                OnContinue();
                return;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

            onStart?.Invoke();
            onFinishDialog = onFinish;

            this.data = data;
            currentSentence = 0;
            _content.Text.text = "";

            _container.SetActive(true);
            _animator.SetBool(IS_OPEN_KEY, true);
        }

        private IEnumerator TypeDialogText()
        {
            _content.Text.text = string.Empty;

            var text = CurrentSentence.Value;
            foreach (var letter in text)
            {
                _content.Text.text += letter;
                yield return new WaitForSeconds(_textSpeed);
            }

            typingRoutine = null;
        }

        public void OnSkip()
        {
            if (typingRoutine == null)
                return;

            StopTypeAnimation();
            _content.Text.text = data.Sentences[currentSentence].Value;
        }

        public void OnContinue()
        {
            if (typingRoutine != null)
            {
                OnSkip();
                return;
            }

            StopTypeAnimation();
            currentSentence++;

            var isDialogComplete = currentSentence >= data.Sentences.Length;
            if (isDialogComplete)
                HideDialogBox();
            else
                OnStartDialogAnimationComplete();
        }

        private void HideDialogBox()
        {
            _animator.SetBool(IS_OPEN_KEY, false);
        }

        private void StopTypeAnimation()
        {
            if (typingRoutine != null)
                StopCoroutine(typingRoutine);
            typingRoutine = null;
        }

        protected virtual void OnStartDialogAnimationBegin()
        {
            _container.SetActive(true);
        }

        protected virtual void OnStartDialogAnimationComplete()
        {
            //Cursor.visible = true;
            typingRoutine = StartCoroutine(TypeDialogText());
        }

        protected virtual void OnCloseAnimationComplete()
        {
            onFinishDialog?.Invoke();
            //Cursor.visible = false;
            _container.SetActive(false);
        }
    }
}