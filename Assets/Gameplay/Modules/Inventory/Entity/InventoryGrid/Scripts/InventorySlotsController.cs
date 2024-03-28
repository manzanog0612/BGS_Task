using System;
using System.Collections.Generic;

using UnityEngine;

using BGS_Task.Gameplay.Inventory.Entity.Slot;
using BGS_Task.Gameplay.Common.Items;
using BGS_Task.Gameplay.Inventory.Entity.CharacterSlot;

namespace BGS_Task.Gameplay.Inventory.Entity.InventorySlots
{
    public class InventorySlotsController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private SlotController[] slots = null;
        #endregion

        #region PUBLIC_METHODS
        public void Init(Action<ItemConfig, SlotController> onGrabItem, Action<SlotController> onDropItem)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Init(onGrabItem, onDropItem);
            }
        }

        public void Configure(List<ItemConfig> equipedItems, List<ItemConfig> storedItems)
        {
            List<CharacterSlotController> characterSlots = new List<CharacterSlotController>();
            List<SlotController> inventorySlots = new List<SlotController>();

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] is CharacterSlotController characterSlot)
                {
                    characterSlots.Add(characterSlot);
                }
                else
                {
                    inventorySlots.Add(slots[i]);
                }
            }

            for (int i = 0; i < equipedItems.Count; i++)
            {
                CharacterSlotController characterSlot = characterSlots.Find(slot => slot.IsCompatible(equipedItems[i]));
                characterSlot.Configue(equipedItems[i]);
            }

            for (int i = 0; i < storedItems.Count; i++)
            {
                inventorySlots[i].Configue(storedItems[i]);
            }
        }

        public void Refresh()
        {
            List<SlotController> filledSlots = new List<SlotController>();

            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].IsEmpty && slots[i].CanBeRearranged())
                {
                    filledSlots.Add(slots[i]);
                }
            }

            for (int i = 0; i < filledSlots.Count; i++)
            {
                filledSlots[i].transform.SetSiblingIndex(i);
            }
        }
        #endregion
    }
}