using System.Linq;
using InventorySystem.Item;
using UnityEngine;

namespace InventorySystem
{
    public class ItemTypeBackgroundManager : MonoBehaviour
    {
        [SerializeField]
        private ItemTypeBackground[] _backgrounds;

        public ItemTypeBackground GetBackgroundByItemType(ItemType itemType)
        {
            return _backgrounds.First(background => background.Type == itemType);
        }
    }
}