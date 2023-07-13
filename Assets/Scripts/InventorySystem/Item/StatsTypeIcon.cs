using UnityEngine;

namespace InventorySystem.Item
{
    [CreateAssetMenu(fileName = "StatsTypeIcon", menuName = "ScriptableObject/StatsTypeIcon", order = 50)]
    public class StatsTypeIcon : ScriptableObject
    {
        [field:SerializeField]
        public StatsType Type { get; private set; }
        [field:SerializeField]
        public Sprite Icon { get; private set; }
    }
}