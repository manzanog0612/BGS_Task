using UnityEngine;

using BGS_Task.Gameplay.Common.Items;
using BGS_Task.Gameplay.Inventory.Entity.InventorySlots;
using BGS_Task.Gameplay.Inventory.Entity.Slot;
using BGS_Task.Gameplay.Inventory.Entity.MouseFollower;

namespace BGS_Task.Gameplay.Inventory.View
{
    public class InventoryView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private InventorySlotsController gridController = null;
        [SerializeField] private ItemMouseFollower itemMouseFollower = null;
        #endregion

        #region PRIVATE_FIELDS
        private ItemConfig grabbedItem = null;
        private SlotController grabbedItemSlot = null;
        #endregion

        #region PUBLIC_METHODS
        public void Init()
        {
            gridController.Init(OnGrabItem, OnDropItem);
            itemMouseFollower.Init();
        }

        public void OnItemReset()
        {
            grabbedItemSlot.Configue(grabbedItem);
            itemMouseFollower.Toggle(false);
        }
        #endregion

        #region PRIVATE_METHODS
        private void OnGrabItem(ItemConfig item, SlotController slot)
        {
            grabbedItem = item;
            grabbedItemSlot = slot;
            itemMouseFollower.Configure(item.Icon);
        }

        private void OnDropItem(SlotController slot)
        {
            if (slot.IsCompatible(grabbedItem))
            {
                slot.Configue(grabbedItem);
                itemMouseFollower.Toggle(false);
            }
            else
            {
                OnItemReset();
            }

            gridController.Refresh();
        }
        #endregion
    }
}