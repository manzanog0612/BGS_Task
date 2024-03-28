using System;

using BGS_Task.Gameplay.Common.Items;
using BGS_Task.Gameplay.Common.Items.View;

namespace BGS_Task.Gameplay.Inventory.Entity.Slot
{
    public class SlotController : ItemView
    {
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
            if (IsEmpty)
            {
                return;
            }

            onGrabItem.Invoke(item, this);
            Configue(null);
        }

        public void OnDrop()
        {
            onDropItem.Invoke(this);
        }
        #endregion
    }
}
