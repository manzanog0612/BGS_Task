using System;
using System.Collections.Generic;

using UnityEngine;

using BGS_Task.Gameplay.Common.Items.Handler;
using BGS_Task.Gameplay.Common.Items;
using BGS_Task.Gameplay.Common.Event;
using BGS_Task.Gameplay.Store.Entity.StoreGrid;
using BGS_Task.Gameplay.Store.Model;
using BGS_Task.Gameplay.Store.View;
using BGS_Task.Gameplay.Inventory.Model;

namespace BGS_Task.Gameplay.Store.Controller
{
    public class StoreController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private StoreGridController inventoryGrid = null;
        [SerializeField] private StoreGridController storeGrid = null;
        [SerializeField] private GameObject blocker = null;
        [SerializeField] private EventTrigger eventTrigger = null;
        [SerializeField] private StoreView storeView = null;
        #endregion

        #region PRIVATE_FIELDS
        private StoreModel model = null;
        private InventoryModel inventoryModel = null;

        private ItemConfig selectedItem = null;

        private bool closeEnough = false;
        private bool open = false;
        #endregion

        #region UNITY_CALLS
        private void Update()
        {
            if (/*closeEnough ||*/ blocker.activeSelf) //means animation is still playing, so we can't interact with the inventory
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (open)
                {
                    storeView.ToggleView(false);
                }
                else
                {
                    List<ItemConfig> inventoryItems = ItemsHandler.Instance.GetItems(inventoryModel.storedItems.items);
                    List<ItemConfig> storeItems = ItemsHandler.Instance.GetItems(model.availableItems.items);
                    inventoryGrid.Configure(inventoryItems);
                    storeGrid.Configure(storeItems);
                    storeView.ToggleView(true);
                }
            }
        }
        #endregion

        #region PUBLIC_METHODS
        public void Init(StoreModel model, InventoryModel inventoryModel, Action<bool> onToggleView)
        {
            this.model = model;
            this.inventoryModel = inventoryModel;

            //eventTrigger.onTriggerEvent += (closeEnough) => this.closeEnough = closeEnough;

            inventoryGrid.Init(OnItemSelected, onItemAction: () => OnItemAction(true));
            storeGrid.Init(OnItemSelected, onItemAction: () => OnItemAction(false));

            onToggleView += (status) => open = status;
            storeView.Init(onToggleView, blocker.SetActive);
        }
        #endregion

        #region PRIVATE_METHODS
        private void OnItemSelected(ItemConfig itemConfig)
        {
            if (model.availableItems.items.Contains(itemConfig.Id))
            {
                inventoryGrid.DeselectItem();
            }
            else
            {
                storeGrid.DeselectItem();
            }

            selectedItem = itemConfig;
        }

        private void OnItemAction(bool sell)
        {
            if (sell)
            {
                storeGrid.AddNewItemToGrid(selectedItem);
                model.availableItems.items.Add(selectedItem.Id);
                inventoryModel.storedItems.items.Remove(selectedItem.Id);
            }
            else
            {
                inventoryGrid.AddNewItemToGrid(selectedItem);
                inventoryModel.storedItems.items.Add(selectedItem.Id);
                model.availableItems.items.Remove(selectedItem.Id);
            }
        }
        #endregion
    }
}