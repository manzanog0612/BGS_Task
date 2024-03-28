using BGS_Task.Gameplay.Common.File;
using BGS_Task.Gameplay.Model;
using BGS_Task.Gameplay.Player.Controller;

using UnityEngine;

namespace BGS_Task.Gameplay.Controller
{
    public class GameplayController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private TextAsset gameplayJson = null;
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
        }
        #endregion
    }
}