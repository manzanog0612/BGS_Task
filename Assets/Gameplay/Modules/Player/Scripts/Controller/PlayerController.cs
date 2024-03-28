using UnityEngine;

using BGS_Task.Gameplay.Player.Model;

using CharacterController = BGS_Task.Gameplay.Character.Controller.CharacterController;
using System.Collections.Generic;
using BGS_Task.Gameplay.Common.Items.Handler;
using BGS_Task.Gameplay.Common.Items;

namespace BGS_Task.Gameplay.Player.Controller
{
    public class PlayerController : CharacterController
    {
        #region EXPOSED_FIELDS
        [SerializeField] private float movementThreshold = 0.1f;
        #endregion

        #region PRIVATE_FIELDS
        private PlayerModel model = null;
        private bool movementEnabled = true;
        private List<string> defaultItems = null;
        #endregion

        #region UNITY_CALLS
        private void FixedUpdate()
        {
            if (!movementEnabled)
            {
                characterView.DoMovementAnimation(Vector2.zero);
                return;
            }

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

            characterView.DoMovementAnimation(movement);
        }
        #endregion

        #region PUBLIC_METHODS
        public void Init(PlayerModel model, List<string> defaultItems)
        {
            this.model = model;
            this.defaultItems = defaultItems;

            Init();
            RefreshView();
        }

        public void ToggleMovement(bool status)
        {
            movementEnabled = status;
        }

        public void RefreshView()
        {
            List<string> equipedParts = new List<string>();

            equipedParts.AddRange(model.inventory.equipedItems.items);

            if (equipedParts.Count == 0)
            {
                equipedParts.AddRange(defaultItems);
            }
            else if (equipedParts.Count < defaultItems.Count)
            {
                for (int i = 0; i < defaultItems.Count; i++)
                {
                    string category = defaultItems[i].Split('_')[0];
                    bool found = false;

                    for (int j = 0; j < equipedParts.Count; j++)
                    {
                        if (equipedParts[j].Contains(category))
                        {
                            found = true; 
                            break;
                        }
                    }

                    if (!found)
                    {
                        equipedParts.Add(defaultItems[i]);
                    }
                }
            }

            Init();
            Configure(equipedParts);
        }
        #endregion
    }
}
