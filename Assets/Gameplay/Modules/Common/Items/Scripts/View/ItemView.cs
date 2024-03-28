using UnityEngine;
using UnityEngine.UI;

namespace BGS_Task.Gameplay.Common.Items.View
{
    public class ItemView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] protected Image icon = null;
        #endregion

        #region PRIVATE_FIELDS
        protected ItemConfig item = null;
        #endregion

        #region PUBLIC_METHODS
        public virtual void Configue(ItemConfig itemConfig)
        {
            item = itemConfig;

            if (itemConfig == null)
            {
                ToggleIcon(false);
            }
            else
            {
                SetIcon(itemConfig.Icon);
            }
        }
        #endregion

        #region PROTECTED_METHODS
        protected void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
            ToggleIcon(true);
        }

        protected void ToggleIcon(bool status)
        {
            icon.gameObject.SetActive(status);
        }
        #endregion
    }
}
