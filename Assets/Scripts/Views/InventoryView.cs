using System;
using System.Collections.Generic;
using Api;
using Api.Responses;
using InventorySystem.Item;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Views
{
    public class InventoryView : ScreenViewWithItemsBase
    {
        public event Action<string> OnItemDeletedSuccessful;

        [SerializeField]
        private UnityEvent<ItemType> _itemDeletedEvent;

        private Dictionary<ItemModel, int> _itemModels;

        public override void ShowScreen()
        {
            ShowBlackout();
            WebApi.Instance.InventoryApi.SendGetAllUserItemsRequest(OnGetItems, OnError);
        }

        [UsedImplicitly]
        public void TryDeleteItem()
        {
            ShowBlackout();
            WebApi.Instance.InventoryApi.SendDeleteItemByIdRequest(_currentItem.Id, OnItemDelete, OnError);
        }

        protected override void InitializeItems()
        {
            foreach (var itemModel in _itemModels)
            {
                var background = _backgroundManager.GetBackgroundByItemType(itemModel.Key.ItemType);
                var itemView = Instantiate(_itemPrefab, _itemRoot);
                itemView.Initialize(background.Background, itemModel.Key, ShowItemInfo, itemModel.Value);
                _itemViews.Add(itemView);
            }
            
            _itemsGroupSorter.Initialize(_itemViews);
        }

        protected override void OnError(string message)
        {
            ClearScreen();
            ShowItemNotSelected();
            base.OnError(message);
        }

        private void OnGetItems(InventoryResponse response)
        {
            ClearScreen();
            ShowItemNotSelected();
            HideBlackout();

            _itemModels = response.UserItems;
            InitializeItems();
        }
        
        private void OnItemDelete(InventoryResponse response)
        {
            ClearScreen();
            ShowItemNotSelected();
            HideBlackout();
            
            DeleteItem(_currentItem);
            OnItemDeletedSuccessful?.Invoke(response.Content);
            
            InitializeItems();
            _itemDeletedEvent?.Invoke(_itemsGroupSorter.SelectedMenu);
        }

        private void DeleteItem(ItemModel itemModel)
        {
            var value = _itemModels[itemModel];

            if (value > 1)
            {
                value--;
                _itemModels[itemModel] = value;
            }
            else
            {
                _itemModels.Remove(itemModel);
            }

            if (_itemModels.Count == 0)
            {
                OnError("Items not found");
            }
        }
    }
}