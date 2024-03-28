using System;
using System.Collections.Generic;

namespace BGS_Task.Gameplay.Inventory.Model
{
    [Serializable]
    public class ItemsModel
    {
        public List<string> items = new List<string>();

        public void AddItem(string item)
        {
            items.Add(item);
        }

        public void RemoveItem(string item)
        {
            items.Remove(item);
        }
    }
}
