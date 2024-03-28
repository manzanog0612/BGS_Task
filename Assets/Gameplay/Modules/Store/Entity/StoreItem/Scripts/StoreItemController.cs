using System;

using UnityEngine;
using UnityEngine.UI;

using BGS_Task.Gameplay.Common.Items.View;
using BGS_Task.Gameplay.Common.Items;

using TMPro;

namespace BGS_Task.Gameplay.Store.Entity.StoreGrid.Controller
{
    public class StoreItemController : ItemView
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Button btn = null;
        [SerializeField] private Image imgFrame = null;
        [SerializeField] private TextMeshProUGUI txtPrice = null;

        [Header("Frame Colors")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color selectedColor = Color.green;
        #endregion

        #region ACTIONS
        private Action<ItemConfig, StoreItemController> onSelected = null;
        #endregion

        #region PUBLIC_METHODS
        public void Init(Action<ItemConfig, StoreItemController> onSelected)
        {
            this.onSelected = onSelected;

            btn.onClick.AddListener(OnBtnClick);
        }

        public override void Configue(ItemConfig itemConfig)
        {
            base.Configue(itemConfig);

            txtPrice.text = "x" + itemConfig.Price.ToString();
            ToggleSelection(false);
        }

        public void ToggleSelection(bool status)
        {
            imgFrame.color = status ? selectedColor : normalColor;
        }

        public void Toggle(bool status)
        {
            gameObject.SetActive(status);
        }
        #endregion

        #region PRIVATE_METHODS
        private void OnBtnClick()
        {
            ToggleSelection(true);

            onSelected.Invoke(item, this);
        }
        #endregion
    }
}
