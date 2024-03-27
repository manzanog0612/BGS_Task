using UnityEngine;

using CharacterController = BGS_Task.Gameplay.Character.CharacterController;

namespace BGS_Task.Gameplay.Player
{
    public class PlayerController : CharacterController
    {
        #region EXPOSED_FIELDS
        #endregion

        #region PRIVATE_FIELDS
        #endregion

        #region UNITY_CALLS
        private void LateUpdate()
        {
            Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
        #endregion

        #region PUBLIC_METHODS
        #endregion
    }
}
