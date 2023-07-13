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

        public void ShowStats(ItemModel itemModelModel)
        {
            if (itemModelModel.Attack != 0)
            {
                var itemStatsView = Instantiate(_itemStatsViewPrefab, transform);
                var attack = _statsIcons.First(group => group.Type == StatsType.Attack);
                itemStatsView.Initialize(attack, itemModelModel.Attack);
                _stats.Add(itemStatsView);
            }
            
            if (itemModelModel.Defense != 0)
            {
                var itemStatsView = Instantiate(_itemStatsViewPrefab, transform);
                var defense = _statsIcons.First(group => group.Type == StatsType.Defense);
                itemStatsView.Initialize(defense, itemModelModel.Defense);
                _stats.Add(itemStatsView);
            }
            
            if (itemModelModel.Speed != 0)
            {
                var itemStatsView = Instantiate(_itemStatsViewPrefab, transform);
                var speed = _statsIcons.First(group => group.Type == StatsType.Speed);
                itemStatsView.Initialize(speed, itemModelModel.Speed);
                _stats.Add(itemStatsView);
            }
            
            if (itemModelModel.Health != 0)
            {
                var itemStatsView = Instantiate(_itemStatsViewPrefab, transform);
                var health = _statsIcons.First(group => group.Type == StatsType.Health);
                itemStatsView.Initialize(health, itemModelModel.Health);
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