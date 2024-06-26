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
using BGS_Task.Gameplay.Player.Model;
using BGS_Task.Gameplay.Dialog.Config;

namespace BGS_Task.Gameplay.Store.Controller
{
    public class StoreController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private StoreGridController inventoryGrid = null;
        [SerializeField] private StoreGridController storeGrid = null;
        [SerializeField] private GameObject blocker = null;
        [SerializeField] private ClosenessEventTrigger eventTrigger = null;
        [SerializeField] private StoreView storeView = null;

        [Header("Event Config")]
        [SerializeField] private DialogConfig dialogConfig = null;
        #endregion

        #region PRIVATE_FIELDS
        private StoreModel model = null;
        private InventoryModel inventoryModel = null;
        private PlayerModel playerModel = null;

        private ItemConfig selectedItem = null;

        private bool closeEnough = false;
        private bool open = false;
        #endregion

        #region ACTION
        private Action onCurrencyValuesChanged = null;
        #endregion

        #region UNITY_CALLS
        private void Update()
        {
            if (!closeEnough || blocker.activeSelf) //means animation is still playing, so we can't interact with the inventory
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Toggle();
            }
        }
        #endregion

        #region PUBLIC_METHODS
        public void Init(StoreModel model, PlayerModel playerModel, Action<bool> onToggleView, Action onCurrencyValuesChanged)
        {
            this.model = model;
            this.inventoryModel = playerModel.inventory;
            this.playerModel = playerModel;

            this.onCurrencyValuesChanged = onCurrencyValuesChanged;

            eventTrigger.onTriggerEvent += (closeEnough) => this.closeEnough = closeEnough;

            inventoryGrid.Init(OnItemSelected, onItemAction: () => OnItemAction(true));
            storeGrid.Init(OnItemSelected, onItemAction: () => OnItemAction(false));

            onToggleView += (status) => open = status;
            storeView.Init(onToggleView, blocker.SetActive);

            storeView.ToggleNotEnoughCurrency(false);
        }
        #endregion

        #region PRIVATE_METHODS
        private void Toggle()
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

        private void OnItemSelected(ItemConfig itemConfig)
        {
            if (model.availableItems.items.Contains(itemConfig.Id)) //is store item
            {
                inventoryGrid.DeselectItem();

                if (playerModel.currency < itemConfig.Price)
                {
                    storeGrid.DeselectItem();
                    storeView.ToggleNotEnoughCurrency(true);
                    return;
                }
            }
            else //is inventory item
            {
                storeGrid.DeselectItem();
            }

            selectedItem = itemConfig;
            storeView.ToggleNotEnoughCurrency(false);
        }

        private void OnItemAction(bool sell)
        {
            if (sell)
            {
                storeGrid.AddNewItemToGrid(selectedItem);
                model.availableItems.items.Add(selectedItem.Id);
                inventoryModel.storedItems.items.Remove(selectedItem.Id);
                playerModel.currency += selectedItem.Price;
            }
            else
            {
                inventoryGrid.AddNewItemToGrid(selectedItem);
                inventoryModel.storedItems.items.Add(selectedItem.Id);
                model.availableItems.items.Remove(selectedItem.Id);
                playerModel.currency -= selectedItem.Price;
            }

            onCurrencyValuesChanged.Invoke();
        }
        #endregion
    }
}