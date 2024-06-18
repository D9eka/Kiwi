using UnityEngine;

namespace Components.UI
{
    public class KeyCardUI : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(MyGameManager.HaveKeyCard);
            MyGameManager.OnChangeHaveKeyCardState += MyGameManager_OnChangeHaveKeyCardState;
        }

        private void MyGameManager_OnChangeHaveKeyCardState(object sender, bool e)
        {
            gameObject.SetActive(e);
        }

        private void OnDestroy()
        {
            MyGameManager.OnChangeHaveKeyCardState -= MyGameManager_OnChangeHaveKeyCardState;
        }
    }
}
