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

        #region PUBLIC_METHODS
        public override void Configue(ItemConfig itemConfig)
        {
            base.Configue(itemConfig);
            imgCharacterPart.sprite = itemConfig.Icon;
        }

        public override bool CanBeRearranged()
        {
            return false;
        }

        public override bool IsCompatible(ItemConfig itemConfig)
        {
            if (itemConfig is CharacterItemConfig characterItemConfig)
            {
                return base.IsCompatible(itemConfig) && characterItemConfig.AvatarItemType == slotArea;
            }
            else
            {
                return false;
            }           
        }
        #endregion
    }
}
