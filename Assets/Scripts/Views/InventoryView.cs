using System.Collections.Generic;
using Api;
using Api.Responses;
using Core;
using InventorySystem.Item;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Views
{
    public class InventoryView : ScreenViewWithItemsBase
    {
        [SerializeField]
        private UnityEvent<ItemType> _itemDeleted;

        private Dictionary<ItemModel, int> _itemModels = new();

        public override void ShowScreen()
        {
            if (_itemViews.Count != 0)
            {
                ClearScreen();
                ShowItemNotSelected();
                InitializeItems();
                return;
            }
            
            ShowBlackout();
            WebApi.Instance.InventoryApi.SendGetAllUserItemsRequest(OnGetUserItems, OnError);
        }

        [UsedImplicitly]
        public void TryDeleteItem()
        {
            ShowBlackout();
            WebApi.Instance.InventoryApi
                .SendDeleteItemByIdRequest(_currentItem.Id, OnItemDeleted, OnError);
        }
        
        public void AddItem(ItemModel itemModel)
        {
            if (_itemModels.Count == 0)
            {
                return;
            }

            if (_itemModels.ContainsKey(itemModel))
            {
                var value = _itemModels[itemModel];
                value++;
                _itemModels[itemModel] = value;
            }
            else
            {
                _itemModels.Add(itemModel, 1);
            }
        }

        protected override void InitializeItems()
        {
            foreach (var itemModel in _itemModels)
            {
                var background = _backgroundManager.GetBackgroundByItemType(itemModel.Key.ItemType);
                var itemView = Instantiate(_itemPrefab, _itemRoot);
                itemView.Initialize(background.Background, itemModel.Key,
                    ShowItemInfo, itemModel.Value);
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

        private void OnGetUserItems(InventoryResponse response)
        {
            ClearScreen();
            ShowItemNotSelected();
            HideBlackout();

            _itemModels = response.UserItems;
            InitializeItems();
        }
        
        private void OnItemDeleted(InventoryResponse response)
        {
            ClearScreen();
            ShowItemNotSelected();
            HideBlackout();
            
            DeleteItem(_currentItem);
            NotificationsManager.Instance.ShowNotification(response.Content);
            
            InitializeItems();
            _itemDeleted?.Invoke(_itemsGroupSorter.SelectedMenu);
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