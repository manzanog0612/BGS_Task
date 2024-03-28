using UnityEngine;

namespace BGS_Task.Gameplay.Scenery
{
    //[CreateAssetMenu(fileName = "Tile_", menuName = "ScriptableObjects/Scenery/Tile", order = 1)]
    public class TileData
    {
        [SerializeField] private string id = null;
        [SerializeField] private Sprite sprite = null;

        public string Id { get => id; }
        public Sprite Sprite { get => sprite; }
    }
}

