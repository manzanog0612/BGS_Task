using System;

using UnityEngine;

using BGS_Task.Gameplay.Inventory.Entity.MouseFollower;
using BGS_Task.Gameplay.Common.Panel;

namespace BGS_Task.Gameplay.Inventory.View
{
    public class InventoryView : PanelAnimatedView
    {
        #region EXPOSED_FIELDS
        [SerializeField] private ItemMouseFollower itemMouseFollower = null;
        #endregion

        #region PUBLIC_METHODS
        public override void Init(Action<bool> onToggleView, Action<bool> toggleBlocker)
        {
            base.Init(onToggleView, toggleBlocker);

            itemMouseFollower.Init();
            TurnPointerOff();
        }

        public void ConfigurePointer(Sprite sprite)
        {
            itemMouseFollower.Configure(sprite);
        }

        public void TurnPointerOff()
        {
            itemMouseFollower.Toggle(false);
        }
        #endregion
    }
}