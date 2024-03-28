using System;

using UnityEngine;
using UnityEngine.UI;

namespace BGS_Task.Gameplay.Common.Panel
{
    public class PanelAnimatedView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Transform holder = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] Button btnClose = null;
        [SerializeField] private PanelViewCalls panelViewCalls = null;
        #endregion

        #region CONSTANTS
        private const string openAnim = "Open";
        #endregion

        #region ACTIONS
        private Action<bool> onToggleView, toggleBlocker;
        #endregion

        #region PUBLIC_METHODS
        public virtual void Init(Action<bool> onToggleView, Action<bool> toggleBlocker)
        {
            this.onToggleView = onToggleView;
            this.toggleBlocker = toggleBlocker;

            TogglePanel(false);

            btnClose.onClick.AddListener(() => ToggleView(false));

            panelViewCalls.onOpened += OnOpenend;
            panelViewCalls.onClosed += OnClose;
        }

        public void ToggleView(bool status)
        {
            toggleBlocker.Invoke(true);

            if (status)
            {
                TogglePanel(status);
                animator.SetBool(openAnim, true);
            }
            else
            {
                animator.SetBool(openAnim, false);
            }
        }
        #endregion

        #region PRIVATE_METHODS
        private void TogglePanel(bool status)
        {
            holder.gameObject.SetActive(status);
            onToggleView?.Invoke(status);
        }

        #region ANIMATOR_METHODS
        private void OnClose()
        {
            TogglePanel(false);
            toggleBlocker.Invoke(false);
        }
        private void OnOpenend()
        {
            toggleBlocker.Invoke(false);
        }
        #endregion
        #endregion
    }
}