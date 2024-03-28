using System;

using UnityEngine;
using UnityEngine.UI;

using BGS_Task.Gameplay.Common.Items;

namespace BGS_Task.Gameplay.Inventory.Entity.Slot
{
    public class SlotController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Image icon = null;
        #endregion

        #region PRIVATE_FIELDS
        private ItemConfig item = null;
        #endregion

        #region ACTIONS
        private Action<ItemConfig, SlotController> onGrabItem = null;
        private Action<SlotController> onDropItem = null;
        #endregion

        #region PROPERTIES
        public bool IsEmpty => item == null;
        #endregion

        #region PUBLIC_METHODS
        public void Init(Action<ItemConfig, SlotController> onGrabItem, Action<SlotController> onDropItem)
        {
            this.onGrabItem = onGrabItem;
            this.onDropItem = onDropItem;

            Configue(null);
        }

        public virtual void Configue(ItemConfig itemConfig)
        {
            item = itemConfig;

            if (itemConfig == null)
            {
                ToggleIcon(false);
            }
            else
            {
                SetIcon(itemConfig.Icon);
            }
        }

        public virtual bool IsCompatible(ItemConfig itemConfig)
        {
            return IsEmpty;
        }

        public virtual bool CanBeRearranged()
        {             
            return true;
        }

        public void OnGrab()
        {
            onGrabItem.Invoke(item, this);
            Configue(null);
        }

        public void OnDrop()
        {
            onDropItem.Invoke(this);
        }
        #endregion

        #region PRIVATE_METHODS
        private void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
            ToggleIcon(true);
        }

        private void ToggleIcon(bool status)
        {
            icon.gameObject.SetActive(status);
        }
        #endregion
    }
}
