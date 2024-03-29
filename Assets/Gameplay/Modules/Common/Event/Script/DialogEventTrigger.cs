using UnityEngine;

using BGS_Task.Gameplay.Dialog.View;
using BGS_Task.Gameplay.Dialog.Config;

namespace BGS_Task.Gameplay.Common.Event
{
    public class DialogEventTrigger : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private DialogView dialogView = null;
        [SerializeField] private DialogConfig dialogConfig = null;
        #endregion

        #region PRIVATE_FIELDS
        private const string playerTag = "Player";
        private bool canTrigger = false;
        #endregion

        #region UNITY_CALLS
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && canTrigger && !dialogView.AlreadyOpen)
            {
                dialogView.ShowDialog(dialogConfig);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(playerTag))
            {
                canTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(playerTag))
            {
                canTrigger = false;
            }
        }
        #endregion
    }
}
