using System.Collections.Generic;

using UnityEngine;

using BGS_Task.Gameplay.Character.View;
using BGS_Task.Gameplay.Common.Items.Handler;
using BGS_Task.Gameplay.Common.Items;

namespace BGS_Task.Gameplay.Character.Controller
{
    public class CharacterController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] protected CharacterView characterView = null;
        [SerializeField] private float speed = 1f;
        #endregion

        #region PRIVATE_FIELDS
        private Rigidbody2D body = null;
        #endregion

        #region UNITY_CALLS
        private void Start()
        {
            body = GetComponent<Rigidbody2D>();
        }
        #endregion

        #region PUBLIC_METHODS
        public virtual void Init()
        {
            body = GetComponent<Rigidbody2D>();
        }

        public void Configure(List<string> equipedParts)
        {
            List<ItemConfig> items = ItemsHandler.Instance.GetItems(equipedParts);

            List<CharacterItemConfig> equipedPartsConfig = new List<CharacterItemConfig>();

            for (int i = 0; i < items.Count; i++)
            {
                equipedPartsConfig.Add(items[i] as CharacterItemConfig);
            }

            characterView.Configure(equipedPartsConfig);
        }

        public void Move(Vector2 movement)
        {
            Vector2 currentVelocity = body.velocity;

            if ((movement.x > 0 && body.velocity.x < 0) || (movement.x < 0 && body.velocity.x > 0))
            {
                currentVelocity.x = 0;
                currentVelocity.y = 0;
            }
            
            if ((movement.y > 0 && body.velocity.y < 0) || (movement.y < 0 && body.velocity.y > 0))
            {
                currentVelocity.x = 0;
                currentVelocity.y = 0;
            }

            body.velocity = currentVelocity;
            body.AddForce(movement * speed * Time.fixedDeltaTime);
        }
        #endregion
    }
}