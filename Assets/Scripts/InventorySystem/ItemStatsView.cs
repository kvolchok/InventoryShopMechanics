using InventorySystem.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class ItemStatsView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private TextMeshProUGUI _value;

        public void Initialize(StatsTypeIcon statsType, int value)
        {
            _icon.sprite = statsType.Icon;
            _name.text = statsType.Type.ToString();
            _value.text = value.ToString();
        }
    }
}