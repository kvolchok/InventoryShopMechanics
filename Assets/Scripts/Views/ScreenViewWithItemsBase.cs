using System;
using System.Collections.Generic;
using InventorySystem;
using InventorySystem.Item;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public abstract class ScreenViewWithItemsBase : MonoBehaviour
    {
        public event Action<string> ErrorEvent;
        
        protected readonly List<ItemView> _itemViews = new();

        [SerializeField]
        protected GameObject _blackout;
        [SerializeField]
        protected ItemView _itemPrefab;
        [SerializeField]
        protected Transform _itemRoot;
        
        [SerializeField]
        protected ItemTypeBackgroundManager _backgroundManager;
        [SerializeField]
        protected ItemsGroupSorter _itemsGroupSorter;

        [SerializeField]
        private ItemStatsManager _itemStatsManager;
        [SerializeField]
        private TextMeshProUGUI _itemName;
        [SerializeField]
        private TextMeshProUGUI _itemDescription;
        [SerializeField]
        private Image _itemIcon;

        [SerializeField]
        private Button _actionWithItems;
        
        protected ItemModel _currentItem;

        public void Initialize()
        {
            _itemsGroupSorter.OnItemsShowed += ShowItemNotSelected;
        }

        public abstract void ShowScreen();

        protected abstract void InitializeItems();

        protected virtual void ShowItemInfo(ItemModel itemModel)
        {
            _currentItem = itemModel;
            _itemStatsManager.HideStats();
            
            _itemName.text = _currentItem.Name;
            _itemDescription.text = _currentItem.Description;
            _itemIcon.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(_currentItem.SpritePath);
            _itemIcon.gameObject.SetActive(true);
            
            _itemStatsManager.ShowStats(_currentItem);
            _actionWithItems.interactable = true;
        }
        
        protected virtual void OnError(string message)
        {
            HideBlackout();
            ErrorEvent?.Invoke(message);
        }

        protected void ClearScreen()
        {
            foreach (var itemView in _itemViews)
            {
                Destroy(itemView.gameObject);
            }
            
            _itemViews.Clear();
        }
        
        protected void ShowItemNotSelected()
        {
            _itemStatsManager.HideStats();
            
            _itemName.text = "Item not selected";
            _itemDescription.text = "";
            _itemIcon.gameObject.SetActive(false);
            _actionWithItems.interactable = false;
        }

        protected void ShowBlackout()
        {
            _blackout.SetActive(true);
        }
        
        protected void HideBlackout()
        {
            _blackout.SetActive(false);
        }

        private void OnDestroy()
        {
            _itemsGroupSorter.OnItemsShowed -= ShowItemNotSelected;
        }
    }
}