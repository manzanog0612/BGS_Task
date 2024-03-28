using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

using BGS_Task.Gameplay.Common.Items;
using BGS_Task.Gameplay.Store.Entity.StoreGrid.Controller;

using TMPro;

namespace BGS_Task.Gameplay.Store.Entity.StoreGrid
{
    public class StoreGridController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private GameObject itemPrefab = null;
        [SerializeField] private Transform itemsHolder = null;
        [SerializeField] private Button btnAction  = null;
        [SerializeField] private Transform actionInfoHolder = null;
        [SerializeField] private TextMeshProUGUI txtPrice = null;
        //[SerializeField] private Transform slideBar = null;
        #endregion

        #region PRIVATE_FIELDS
        private ObjectPool<StoreItemController> itemsPool = null;
        private List<StoreItemController> activeItems = new List<StoreItemController>();

        private StoreItemController selectedItem = null;
        #endregion

        #region ACTIONS
        private Action<ItemConfig> onItemSelected = null;
        private Action onItemAction = null;
        #endregion

        #region CONSTANTS
        private int minAmountForScroll = 10;
        #endregion

        #region PUBLIC_METHODS
        public void Init(Action<ItemConfig> onItemSelected, Action onItemAction)
        {
            this.onItemSelected = onItemSelected;
            this.onItemAction = onItemAction;

            itemsPool = new ObjectPool<StoreItemController>(CreateItem, OnGetItem, OnReleaseItem);

            btnAction.onClick.AddListener(DoActionWithItem);
        }

        public void Configure(List<ItemConfig> itemConfigs)
        {
            //slideBar.gameObject.SetActive(itemConfigs.Count >= minAmountForScroll);

            for (int i = activeItems.Count - 1; i >= 0; i--)
            {
                itemsPool.Release(activeItems[i]);
            }

            for (int i = 0; i < itemConfigs.Count; i++)
            {
                StoreItemController sellingItemController = itemsPool.Get();
                sellingItemController.Configue(itemConfigs[i]);
            }

            DeselectItem();
        }

        public void AddNewItemToGrid(ItemConfig itemConfig)
        {
            StoreItemController sellingItemController = itemsPool.Get();
            sellingItemController.Configue(itemConfig);
        }

        public void DeselectItem()
        {
            selectedItem?.ToggleSelection(false);
            selectedItem = null;
            actionInfoHolder.gameObject.SetActive(false);
        }
        #endregion

        #region PRIVATE_METHODS
        private void DoActionWithItem()
        {
            itemsPool.Release(selectedItem);
            selectedItem = null;
            actionInfoHolder.gameObject.SetActive(false);
            onItemAction.Invoke();
        }

        private void OnSelectItem(ItemConfig itemConfig, StoreItemController sellingItem)
        {
            if (selectedItem == sellingItem)
            {
                selectedItem?.ToggleSelection(false);
                return;
            }

            selectedItem?.ToggleSelection(false);
            selectedItem = sellingItem;

            actionInfoHolder.gameObject.SetActive(true);
            txtPrice.text = "x" + itemConfig.Price.ToString();

            onItemSelected.Invoke(itemConfig);
        }

        #region POOLING
        private StoreItemController CreateItem()
        {
            StoreItemController sellingItemController = Instantiate(itemPrefab, itemsHolder).GetComponent<StoreItemController>();
            sellingItemController.Init(OnSelectItem);
            return sellingItemController;
        }

        private void OnGetItem(StoreItemController sellingItemController)
        {
            sellingItemController.Toggle(true);
            activeItems.Add(sellingItemController);
        }

        private void OnReleaseItem(StoreItemController sellingItemController)
        {
            sellingItemController.Toggle(false);
            activeItems.Remove(sellingItemController);
        }
        #endregion
        #endregion

    }
}