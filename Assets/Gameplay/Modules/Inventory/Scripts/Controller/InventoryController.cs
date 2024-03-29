using System;
using System.Collections.Generic;

using UnityEngine;

using BGS_Task.Gameplay.Inventory.View;
using BGS_Task.Gameplay.Inventory.Model;
using BGS_Task.Gameplay.Common.Items;
using BGS_Task.Gameplay.Common.Items.Handler;
using BGS_Task.Gameplay.Inventory.Entity.InventorySlots;
using BGS_Task.Gameplay.Inventory.Entity.Slot;
using BGS_Task.Gameplay.Inventory.Entity.CharacterSlot;

namespace BGS_Task.Gameplay.Inventory.Controller
{
    public class InventoryController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private InventoryView inventoryView = null;
        [SerializeField] private InventorySlotsController gridController = null;
        [SerializeField] private GameObject blocker = null;
        #endregion

        #region PRIVATE_FIELDS
        private InventoryModel inventoryModel = null;

        private ItemConfig grabbedItem = null;
        private SlotController grabbedItemSlot = null;

        private bool open = false;
        #endregion

        #region ACTIONS
        private Action onUpdateCharacterView = null;
        #endregion

        #region UNITY_CALLS
        private void Update()
        {
            if (blocker.activeSelf) //means animation is still playing, so we can't interact with the inventory
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (open)
                {
                    inventoryView.ToggleView(false);
                }
                else
                {
                    List<ItemConfig> equipedItems = ItemsHandler.Instance.GetItems(inventoryModel.equipedItems.items);
                    List<ItemConfig> storedItems = ItemsHandler.Instance.GetItems(inventoryModel.storedItems.items);
                    gridController.Configure(equipedItems, storedItems);
                    inventoryView.ToggleView(true);
                }
            }
        }
        #endregion

        #region PUBLIC_METHODS
        public void Init(InventoryModel inventoryModel, List<string> defaultItems, Action<bool> onToggleView, Action onUpdateCharacterView)
        {
            this.inventoryModel = inventoryModel;
            this.onUpdateCharacterView = onUpdateCharacterView;

            List<ItemConfig> equipedItems = ItemsHandler.Instance.GetItems(inventoryModel.equipedItems.items);
            List<ItemConfig> storedItems = ItemsHandler.Instance.GetItems(inventoryModel.storedItems.items);
            List<ItemConfig> defaultEquipesItems = ItemsHandler.Instance.GetItems(defaultItems);

            gridController.Init(OnGrabItem, OnDropItem, defaultEquipesItems);

            onToggleView += (status) => open = status;

            inventoryView.Init(onToggleView, blocker.SetActive);
        }

        public void OnItemReset()
        {
            if (grabbedItemSlot == null)
            {
                return;
            }

            grabbedItemSlot.Configue(grabbedItem);
            inventoryView.TurnPointerOff();
        }
        #endregion

        #region PRIVATE_METHODS
        private void OnGrabItem(ItemConfig item, SlotController slot)
        {
            grabbedItem = item;
            grabbedItemSlot = slot;
            inventoryView.ConfigurePointer(item.Icon);
        }

        private void OnDropItem(SlotController slot)
        {
            if (slot != grabbedItemSlot && slot.IsCompatible(grabbedItem))
            {
                //item was equiped just now
                if (slot is CharacterSlotController)
                {
                    inventoryModel.equipedItems.items.Add(grabbedItem.Id);
                    inventoryModel.storedItems.items.Remove(grabbedItem.Id);
                }
                else if(grabbedItemSlot is CharacterSlotController)
                {
                    inventoryModel.storedItems.items.Add(grabbedItem.Id);
                    inventoryModel.equipedItems.items.Remove(grabbedItem.Id);                   
                }

                slot.Configue(grabbedItem);
                inventoryView.TurnPointerOff();
            }
            else
            {
                OnItemReset();
            }

            onUpdateCharacterView.Invoke();

            gridController.Refresh();
        }
        #endregion
    }
}
