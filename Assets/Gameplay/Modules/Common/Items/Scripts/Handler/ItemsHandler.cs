using System.Collections.Generic;

using UnityEngine;

namespace BGS_Task.Gameplay.Common.Items.Handler
{
    public class ItemsHandler : MonoBehaviour
    {
        #region SINGLETON
        private static ItemsHandler instance = null;
        public static ItemsHandler Instance { get => instance;}

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region EXPOSED_FIELDS
        [SerializeField] private ItemConfig[] items = null;
        #endregion

        #region PUBLIC_METHODS
        public ItemConfig GetItem(string id)
        { 
            return System.Array.Find(items, item => item.Id == id);
        }

        public List<ItemConfig> GetItems(List<string> id)
        {
            List<ItemConfig> items = new List<ItemConfig>();

            for (int i = 0; i < id.Count; i++)
            {
                ItemConfig item = GetItem(id[i]);

                if (item != null)
                {
                    items.Add(item);
                }
                else
                {
                    Debug.LogError("Item with id: " + id[i] + " not found");
                }
            }

            return items;
        }
        #endregion
    }
}

