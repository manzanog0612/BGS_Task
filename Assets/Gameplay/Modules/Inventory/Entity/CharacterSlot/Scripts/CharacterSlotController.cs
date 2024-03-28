using UnityEngine;
using UnityEngine.UI;

using BGS_Task.Gameplay.Common.Items.Handler;
using BGS_Task.Gameplay.Inventory.Entity.Slot;
using BGS_Task.Gameplay.Common.Items;

namespace BGS_Task.Gameplay.Inventory.Entity.CharacterSlot
{
    public class CharacterSlotController : SlotController
    {
        #region EXPOSED_FIELDS
        [SerializeField] private AVATAR_ITEM_TYPE slotArea = AVATAR_ITEM_TYPE.BOTTOM;
        [SerializeField] private Image imgCharacterPart = null;
        #endregion

        #region PRIVATE_FIELDS
        private ItemConfig emptyItemConfig = null;
        #endregion

        #region PUBLIC_METHODS
        public override void Configue(ItemConfig itemConfig)
        {
            base.Configue(itemConfig);

            if (itemConfig != null)
            {
                imgCharacterPart.sprite = itemConfig.Icon;
                imgCharacterPart.gameObject.SetActive(true);
            }
            else
            {
                if (emptyItemConfig != null)
                {
                    imgCharacterPart.sprite = emptyItemConfig.Icon;
                    imgCharacterPart.gameObject.SetActive(true);
                }
                else
                {
                    imgCharacterPart.gameObject.SetActive(false);
                }
            }
        }

        public void SetEmptyConfig(ItemConfig emptyItemConfig)
        {
            this.emptyItemConfig = emptyItemConfig;
        }

        public override bool CanBeRearranged()
        {
            return false;
        }

        public override bool IsCompatible(ItemConfig itemConfig)
        {
            return base.IsCompatible(itemConfig) && IsSameType(itemConfig);
        }

        public bool IsSameType(ItemConfig itemConfig)
        {
            if (itemConfig is CharacterItemConfig characterItemConfig)
            {
                return characterItemConfig.AvatarItemType == slotArea;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
