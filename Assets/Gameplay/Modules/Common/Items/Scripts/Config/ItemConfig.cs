using UnityEngine;

namespace BGS_Task.Gameplay.Common.Items
{
    public enum ITEM_TYPE
    {
        AVATAR_ITEM,
        OTHER
    }

    [CreateAssetMenu(fileName = "ItemConfig_", menuName = "ScriptableObjects/Common/Items/ItemConfig", order = 1)]
    public class ItemConfig : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [SerializeField] private string id = null;
        [SerializeField] private Sprite icon = null;
        [SerializeField] private ITEM_TYPE type = ITEM_TYPE.OTHER;
        #endregion

        #region PROPERTIES
        public string Id { get => id; }
        public Sprite Icon { get => icon; }
        public ITEM_TYPE Type { get => type; }
        #endregion
    }
}
