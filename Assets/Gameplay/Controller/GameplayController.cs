using UnityEngine;

using BGS_Task.Gameplay.Common.File;
using BGS_Task.Gameplay.Inventory.Controller;
using BGS_Task.Gameplay.Model;
using BGS_Task.Gameplay.Player.Controller;
using BGS_Task.Gameplay.Store.Controller;
using BGS_Task.Gameplay.Modules.Currency.View;

namespace BGS_Task.Gameplay.Controller
{
    public class GameplayController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private TextAsset gameplayJson = null;
        [SerializeField] private InventoryController inventoryController = null;
        [SerializeField] private StoreController storeController = null;
        [SerializeField] private CurrencyView currencyView = null;
        #endregion

        #region PRIVATE_FIELDS
        private GameplayModel gameplayModel = null;
        #endregion

        #region UNITY_CALLS
        private void Start()
        {
            string path = Application.persistentDataPath + "/gameplay.json";

            if (!FileHandler.Exists(path))
            {
                GameplayModel gameplayModel = JsonUtility.FromJson<GameplayModel>(gameplayJson.text);
                FileHandler.Save(gameplayModel, path);
                this.gameplayModel = gameplayModel;
            }
            else
            {
                gameplayModel = FileHandler.Load<GameplayModel>(path);
            }

            playerController.Init(gameplayModel.playerModel, gameplayModel.defaultEquipedItems.items);
            inventoryController.Init(gameplayModel.playerModel.inventory, gameplayModel.defaultEquipedItems.items,
                OnToggleInventory, playerController.RefreshView);
            storeController.Init(gameplayModel.storeModel, gameplayModel.playerModel, OnToggleShop, currencyView.Refresh);
            currencyView.Init(gameplayModel.playerModel);
        }
        #endregion

        #region PRIVATE_METHODS
        private void OnToggleInventory(bool status)
        {
            storeController.enabled = !status;
            playerController.ToggleMovement(!status);
        }

        private void OnToggleShop(bool status)
        {
            inventoryController.enabled = !status;
            playerController.ToggleMovement(!status);
        }
        #endregion
    }
}