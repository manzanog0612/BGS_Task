using UnityEngine;

namespace BGS_Task.Gameplay.Dialog.Config
{
    [CreateAssetMenu(fileName = "DialogConfig_", menuName = "ScriptableObjects/Dialog/DialogConfig")]
    public class DialogConfig : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [SerializeField] private string text = null;
        #endregion

        #region PROPERTIES
        public string Text { get => text; }
        #endregion
    }
}
