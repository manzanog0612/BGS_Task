using BGS_Task.Gameplay.Common.File;
using BGS_Task.Gameplay.Common.Items;
using BGS_Task.Gameplay.Inventory.Controller;
using BGS_Task.Gameplay.Model;
using BGS_Task.Gameplay.Player.Controller;
using System.Collections.Generic;
using UnityEngine;

namespace BGS_Task.Gameplay.Controller
{
    public class GameplayController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private TextAsset gameplayJson = null;
        [SerializeField] private InventoryController inventoryController = null;
        [SerializeField] private ItemConfig[] items = null; //debug
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

            //debug

            List<string> ids = new List<string>();

            for (int i = 0; i < items.Length; i++)
            {
                ids.Add(items[i].Id);
            }

            gameplayModel.playerModel.inventory.storedItems.items.AddRange(ids);
            //----

            playerController.Init(gameplayModel.playerModel, gameplayModel.defaultEquipedItems.items);
            inventoryController.Init(gameplayModel.playerModel.inventory, gameplayModel.defaultEquipedItems.items,
                onToggleView: (status) =>
                {
                    playerController.ToggleMovement(!status);
                },
                playerController.RefreshView);
        }
        #endregion
    }
}