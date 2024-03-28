using UnityEngine;

namespace BGS_Task.Gameplay.Character
{
    public class CharacterController : MonoBehaviour
    {
        #region EXPOSED_FIELDS
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
            body.AddForce(movement * speed);
        }
        #endregion
    }
}