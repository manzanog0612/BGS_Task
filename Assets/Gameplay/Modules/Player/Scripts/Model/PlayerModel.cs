using System;

using BGS_Task.Gameplay.Inventory.Model;

namespace BGS_Task.Gameplay.Player.Model
{
    [Serializable]
    public class PlayerModel
    {
        #region PUBLIC_FIELDS
        public InventoryModel inventory = null;
        public int currency = 0;
        #endregion
    }
}
