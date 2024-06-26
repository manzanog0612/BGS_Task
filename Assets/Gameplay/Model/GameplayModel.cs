using System;

using BGS_Task.Gameplay.Inventory.Model;
using BGS_Task.Gameplay.Player.Model;
using BGS_Task.Gameplay.Store.Model;

namespace BGS_Task.Gameplay.Model
{
    [Serializable]
    public class GameplayModel
    {
        public ItemsModel defaultEquipedItems = null;
        public PlayerModel playerModel = null;
        public StoreModel storeModel = null;
    }
}
