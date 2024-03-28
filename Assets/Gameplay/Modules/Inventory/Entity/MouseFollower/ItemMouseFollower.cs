using UnityEngine;
using UnityEngine.UI;

namespace BGS_Task.Gameplay.Inventory.Entity.MouseFollower
{
    public class ItemMouseFollower : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Image imageToFollowMouse = null;
        #endregion

        #region PRIVATE_FIELDS
        private RectTransform canvasRect = null;
        #endregion

        #region UNITY_CALLS        
        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mousePosition, null, out Vector2 anchoredPosition);
            imageToFollowMouse.rectTransform.anchoredPosition = anchoredPosition;
        }
        #endregion

        #region PUBLIC_METHODS
        public void Init()
        {
            canvasRect = imageToFollowMouse.canvas.GetComponent<RectTransform>();
        }

        public void Configure(Sprite sprite)
        {
            imageToFollowMouse.sprite = sprite;
            Toggle(true);
        }

        public void Toggle(bool status)
        {
            gameObject.SetActive(status);
        }
        #endregion
    }
}
