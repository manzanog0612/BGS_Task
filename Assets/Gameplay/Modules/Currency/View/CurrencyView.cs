using UnityEngine;

using BGS_Task.Gameplay.Player.Model;

using TMPro;

namespace BGS_Task.Gameplay.Modules.Currency.View
{
    public class CurrencyView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private TextMeshProUGUI txtCurrency = null;
        #endregion

        #region PRIVATE_FIELDS
        private PlayerModel playerModel = null;
        #endregion

        #region PUBLIC_METHODS
        public void Init(PlayerModel playerModel)
        {
            this.playerModel = playerModel;
            Refresh();
        }

        public void Refresh()
        {
            txtCurrency.text = playerModel.currency.ToString();
        }
        #endregion
    }
}
