using UnityEngine;
using UnityEngine.U2D.Animation;

namespace BGS_Task.Gameplay.Common.Items.Handler
{
    public enum AVATAR_ITEM_TYPE
    {
        HAIR,
        TOP,
        BOTTOM
    }

    [CreateAssetMenu(fileName = "CharacterItemConfig_", menuName = "ScriptableObjects/Common/Items/CharacterItemConfig", order = 1)]
    public class CharacterItemConfig : ItemConfig
    {
        #region EXPOSED_FIELDS
        [SerializeField] private AVATAR_ITEM_TYPE avatarItemType = AVATAR_ITEM_TYPE.HAIR;
        [SerializeField] private SpriteLibraryAsset spriteLibrary = null;
        #endregion

        #region PROPERTIES
        public AVATAR_ITEM_TYPE AvatarItemType { get => avatarItemType; }
        public SpriteLibraryAsset SpriteLibrary { get => spriteLibrary; }
        #endregion
    }
}
