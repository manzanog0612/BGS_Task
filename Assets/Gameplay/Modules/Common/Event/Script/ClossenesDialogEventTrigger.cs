using UnityEngine;

using BGS_Task.Gameplay.Dialog.View;
using BGS_Task.Gameplay.Dialog.Config;

namespace BGS_Task.Gameplay.Common.Event
{
    public class ClossenesDialogEventTrigger : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private DialogView dialogView = null;
        [SerializeField] private DialogConfig dialogConfig = null;
        #endregion

        #region PRIVATE_FIELDS
        private const string playerTag = "Player";
        #endregion

        #region UNITY_CALLS
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(playerTag))
            {
                dialogView.ShowDialog(dialogConfig, onFinish: () => gameObject.SetActive(false));
            }
        }
        #endregion
    }
}

