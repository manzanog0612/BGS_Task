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
            body.AddForce(movement * speed);
        }
        #endregion
    }
}