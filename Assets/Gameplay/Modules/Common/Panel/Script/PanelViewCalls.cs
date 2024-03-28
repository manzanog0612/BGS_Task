using System;

using UnityEngine;

namespace BGS_Task.Gameplay.Common.Panel
{
    public class PanelViewCalls : MonoBehaviour
    {
        public Action onOpened;
        public Action onClosed;

        public void OnOpened()
        {
            onOpened.Invoke();
        }

        public void OnClosed()
        {
            onClosed.Invoke();
        }
    }
}
