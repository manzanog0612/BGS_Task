using UnityEngine;

using BGS_Task.Gameplay.Common.Panel;

namespace BGS_Task.Gameplay.Store.View
{
    public class StoreView : PanelAnimatedView
    {
        [SerializeField] private Transform notEnoughCurrency = null;

        public void ToggleNotEnoughCurrency(bool status)
        {
            notEnoughCurrency.gameObject.SetActive(status);
        }
    }
}
