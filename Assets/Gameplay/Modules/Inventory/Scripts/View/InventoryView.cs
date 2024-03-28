using UnityEngine;

using BGS_Task.Gameplay.Inventory.Entity.MouseFollower;
using System;
using UnityEngine.UI;

namespace BGS_Task.Gameplay.Inventory.View
{
    public class InventoryView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private ItemMouseFollower itemMouseFollower = null;
        [SerializeField] private Transform holder = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] Button btnClose = null;
        #endregion

        #region CONSTANTS
        private const string openAnim = "Open";
        #endregion

        #region ACTIONS
        private Action<bool> onToggleView, toggleBlocker;
        #endregion

        #region PUBLIC_METHODS
        public void Init(Action<bool> onToggleView, Action<bool> toggleBlocker)
        {
            this.onToggleView = onToggleView;
            this.toggleBlocker = toggleBlocker;

            itemMouseFollower.Init();
            TurnPointerOff();

            TogglePanel(false);

            btnClose.onClick.AddListener(() => ToggleView(false));
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

        public void ConfigurePointer(Sprite sprite)
        {
            itemMouseFollower.Configure(sprite);
        }

        public void TurnPointerOff()
        {
            itemMouseFollower.Toggle(false);
        }

        #region ANIMATOR_METHODS
        public void OnClose()
        {
            TogglePanel(false);
            toggleBlocker.Invoke(false);
        }
        public void OnOpenend()
        {
            toggleBlocker.Invoke(false);
        }
        #endregion
        #endregion

        #region PRIVATE_METHODS
        private void TogglePanel(bool status)
        {
            holder.gameObject.SetActive(status);
            onToggleView.Invoke(status);
        }
        #endregion
    }
}