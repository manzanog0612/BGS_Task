using System.Collections;

using UnityEngine;

using TMPro;
using System;
using System.Net.NetworkInformation;

namespace BGS_Task.Gameplay.Dialog.Entity.TextWritter
{
    public class TextWritterEffect : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private TextMeshProUGUI txt = null;
        [SerializeField] private float typingSpeed = 0.05f;
        #endregion

        #region PRIVATE_FIELDS
        private string currentText = "";
        private string targetText = "";

        private Coroutine typingCoroutine = null;
        private Action onFinish = null;
        #endregion

        #region PROPERTIES
        public bool Typing { get; private set; }
        #endregion

        #region PUBLIC_METHODS
        public void StartTyping(string newText, Action onFinish = null)
        {
            this.onFinish = onFinish;

            if (typingCoroutine != null)
            { 
                StopCoroutine(typingCoroutine); 
            }

            targetText = newText;
            typingCoroutine = StartCoroutine(TypeText());

            IEnumerator TypeText()
            {
                Typing = true;
                currentText = "";
                for (int i = 0; i < targetText.Length; i++)
                {
                    currentText += targetText[i];
                    txt.text = currentText;
                    yield return new WaitForSeconds(typingSpeed);
                }

                Typing = false;
                onFinish?.Invoke();
                typingCoroutine = null;
            }
        }
        public void ForceCompleteTyping()
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            txt.text = targetText;
            Typing = false;
            onFinish?.Invoke();
        }
        #endregion        
    }
}
