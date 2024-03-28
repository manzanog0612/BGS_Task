using System;

namespace BGS_Task.Gameplay.Inventory.Model
{
    [Serializable]
    public class InventoryModel
    {
        public ItemsModel storedItems = null;
        public ItemsModel equipedItems = null;
    }
}
