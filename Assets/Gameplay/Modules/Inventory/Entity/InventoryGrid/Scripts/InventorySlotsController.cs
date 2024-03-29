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
        [SerializeField] private List<SlotController> slots = null;
        #endregion

        #region PRIVATE_FIELDS
        private List<CharacterSlotController> characterSlots = new List<CharacterSlotController>();
        #endregion

        #region PUBLIC_METHODS
        public void Init(Action<ItemConfig, SlotController> onGrabItem, Action<SlotController> onDropItem, List<ItemConfig> defaultEquipesItems)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i] is CharacterSlotController characterSlot)
                {
                    characterSlot.SetEmptyConfig(Array.Find(defaultEquipesItems.ToArray(), item => characterSlot.IsSameType(item)));
                    characterSlots.Add(characterSlot);
                }

                slots[i].Init(onGrabItem, onDropItem);
            }
        }

        public void Configure(List<ItemConfig> equipedItems, List<ItemConfig> storedItems)
        {
            List<SlotController> inventorySlots = new List<SlotController>();

            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i] is not CharacterSlotController)
                {
                    inventorySlots.Add(slots[i]);
                    inventorySlots[i].Configue(null);
                }
            }

            for (int i = 0; i < equipedItems.Count; i++)
            {
                CharacterSlotController characterSlot = characterSlots.Find(slot => slot.IsSameType(equipedItems[i]));
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

            for (int i = slots.Count - 1; i >= 0 ; i--)
            {
                if (!slots[i].IsEmpty && slots[i].CanBeRearranged())
                {
                    filledSlots.Add(slots[i]);
                    slots.Remove(slots[i]);
                }
            }

            filledSlots.Reverse();

            for (int i = 0; i < filledSlots.Count; i++)
            {
                filledSlots[i].transform.SetSiblingIndex(i);
                slots.Insert(i, filledSlots[i]);
            }
        }
        #endregion
    }
}