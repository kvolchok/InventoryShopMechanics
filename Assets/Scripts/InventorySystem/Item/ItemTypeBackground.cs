using UnityEngine;

namespace InventorySystem.Item
{
    [CreateAssetMenu(fileName = "ItemTypeBackground", menuName = "ScriptableObject/ItemTypeBackground", order = 50)]
    public class ItemTypeBackground : ScriptableObject
    {
        [field:SerializeField]
        public ItemType Type { get; private set; }
        [field:SerializeField]
        public Sprite Background { get; private set; }
    }
}