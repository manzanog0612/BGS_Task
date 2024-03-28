using System;

using UnityEngine;

namespace BGS_Task.Gameplay.Common.Event
{
    public class EventTrigger : MonoBehaviour
    {
        public Action<bool> onTriggerEvent = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            onTriggerEvent.Invoke(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            onTriggerEvent.Invoke(false);
        }
    }
}
