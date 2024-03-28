using UnityEngine;

namespace BGS_Task.Gameplay.Scenery
{
	public class TileView : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer renderer = null;

		private string id = null;

        public string Id { get => id; }

        public void Configure(TileData tileData)
		{
			id = tileData.Id;
			renderer.sprite = tileData.Sprite;
		}

		public void ToggleView(bool status)
		{
			gameObject.SetActive(status);
		}
	}
}

