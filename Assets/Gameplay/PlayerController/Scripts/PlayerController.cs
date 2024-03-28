using UnityEngine;

using CharacterController = BGS_Task.Gameplay.Character.CharacterController;

namespace BGS_Task.Gameplay.Player
{
    public class PlayerController : CharacterController
    {
        #region EXPOSED_FIELDS
        [SerializeField] private float movementThreshold = 0.1f;
        #endregion

        #region PRIVATE_FIELDS
        #endregion

        #region UNITY_CALLS
        private void LateUpdate()
        {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            //player moves in only 1 direction at time
            if (Mathf.Abs(movement.x) > movementThreshold)
            {
                movement.y = 0; 
                Move(movement);
            }
            else if (Mathf.Abs(movement.y) > movementThreshold)
            {
                Move(movement);
            }
        }
        #endregion

        #region PUBLIC_METHODS
        #endregion
    }
}
