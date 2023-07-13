using System;
using System.Collections.Generic;
using Api;
using Api.Responses;
using InventorySystem.Item;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Views
{
    public class ShopView : ScreenViewWithItemsBase
    {
        public event Action<int> OnItemBought;
        public event Action<string> ItemBoughtEvent;

        [SerializeField]
        private TextMeshProUGUI _itemPrice;
      
        private List<ItemModel> _itemModels;

        public override void ShowScreen()
        {
            if (_itemViews.Count != 0)
            {
                return;
            }
            
            ShowBlackout();
            WebApi.Instance.ShopApi.SendGetAllGameItemsRequest(OnGetItems, OnError);
        }

        [UsedImplicitly]
        public void TryBuyItem()
        {
            ShowBlackout();
            WebApi.Instance.ShopApi.SendTryBuyItemRequest(_currentItem.Id, OnSuccessBoughtItem, OnError);
        }

        protected override void InitializeItems()
        {
            foreach (var itemModel in _itemModels)
            {
                var background = _backgroundManager.GetBackgroundByItemType(itemModel.ItemType);
                var itemView = Instantiate(_itemPrefab, _itemRoot);
                itemView.Initialize(background.Background, itemModel, ShowItemInfo);
                _itemViews.Add(itemView);
            }
            
            _itemsGroupSorter.Initialize(_itemViews);
        }

        protected override void ShowItemInfo(ItemModel itemModel)
        {
            base.ShowItemInfo(itemModel);
            
            _itemPrice.text = itemModel.Price.ToString();
        }

        private void OnGetItems(ShopResponse response)
        {
            ShowItemNotSelected();
            HideBlackout();
            
            _itemModels = response.GameItems;
            InitializeItems();
        }
        
        private void OnSuccessBoughtItem(ShopResponse response)
        {
            HideBlackout();
            OnItemBought?.Invoke(-response.Item.Price);
            ItemBoughtEvent?.Invoke(response.Content);
        }
    }
}