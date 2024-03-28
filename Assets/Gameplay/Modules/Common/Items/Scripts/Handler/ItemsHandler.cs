using UnityEngine;

namespace BGS_Task.Gameplay.Common.Items.Handler
{
    public class ItemsHandler : MonoBehaviour
    {
        #region SINGLETON
        private ItemsHandler instance = null;
        public ItemsHandler Instance { get => instance;}

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
        public ItemConfig GetItemConfig(string id)
        { 
            return System.Array.Find(items, item => item.Id == id);
        }
        #endregion
    }
}

