using System;

using UnityEngine;
using UnityEngine.UI;

using BGS_Task.Gameplay.Dialog.Entity.TextWritter;
using BGS_Task.Gameplay.Dialog.Config;

namespace BGS_Task.Gameplay.Dialog.View
{
    public class DialogView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameObject holder = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private Button btnClose = null;
        [SerializeField] private TextWritterEffect textWritterEffect = null;
        #endregion

        #region ACTIONS
        private Action<bool> onToggleView = null;
        #endregion

        #region PROPERTIES
        public bool AlreadyOpen => holder.activeSelf;
        #endregion

        #region CONSTANTS
        private const string openAnim = "Open";
        #endregion

        #region PUBLIC_METHODS
        public void Init(Action<bool> onToggleView)
        {
            this.onToggleView = onToggleView;

            btnClose.onClick.AddListener(() =>
            { 
                if (textWritterEffect.Typing)
                {
                    textWritterEffect.ForceCompleteTyping();
                }
                else
                {
                    ToggleView(false);
                }});
        }

        public void ShowDialog(DialogConfig dialogConfig, Action onFinish = null)
        {
            if (!enabled)
            {
                return;
            }

            ToggleView(true);
            textWritterEffect.StartTyping(dialogConfig.Text, onFinish);
        }

        public void ToggleView(bool status)
        {
            if (status)
            {
                holder.SetActive(true);
                onToggleView.Invoke(true);
            }

            animator.SetBool(openAnim, status);
        }

        #region ANIMATOR
        public void OnClose()
        {
            holder.SetActive(false);
            onToggleView.Invoke(false);
        }
        #endregion
        #endregion
    }
}
