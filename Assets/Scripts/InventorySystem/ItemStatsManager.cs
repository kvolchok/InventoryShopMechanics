using System.Collections.Generic;
using System.Linq;
using InventorySystem.Item;
using UnityEngine;

namespace InventorySystem
{
    public class ItemStatsManager : MonoBehaviour
    {
        private readonly List<ItemStatsView> _stats = new();
        
        [SerializeField]
        private StatsTypeIcon[] _statsIcons;
        [SerializeField]
        private ItemStatsView _itemStatsViewPrefab;

        public void ShowStats(ItemModel itemModel)
        {
            if (itemModel.Attack != 0)
            {
                var itemStatsView = Instantiate(_itemStatsViewPrefab, transform);
                var attack = _statsIcons.First(group => group.Type == StatsType.Attack);
                itemStatsView.Initialize(attack, itemModel.Attack);
                _stats.Add(itemStatsView);
            }
            
            if (itemModel.Defense != 0)
            {
                var itemStatsView = Instantiate(_itemStatsViewPrefab, transform);
                var defense = _statsIcons.First(group => group.Type == StatsType.Defense);
                itemStatsView.Initialize(defense, itemModel.Defense);
                _stats.Add(itemStatsView);
            }
            
            if (itemModel.Speed != 0)
            {
                var itemStatsView = Instantiate(_itemStatsViewPrefab, transform);
                var speed = _statsIcons.First(group => group.Type == StatsType.Speed);
                itemStatsView.Initialize(speed, itemModel.Speed);
                _stats.Add(itemStatsView);
            }
            
            if (itemModel.Health != 0)
            {
                var itemStatsView = Instantiate(_itemStatsViewPrefab, transform);
                var health = _statsIcons.First(group => group.Type == StatsType.Health);
                itemStatsView.Initialize(health, itemModel.Health);
                _stats.Add(itemStatsView);
            }
        }

        public void HideStats()
        {
            if (_stats.Count == 0)
            {
                return;
            }
            
            foreach (var itemStatsView in _stats)
            {
                Destroy(itemStatsView.gameObject);
            }
            
            _stats.Clear();
        }
    }
}