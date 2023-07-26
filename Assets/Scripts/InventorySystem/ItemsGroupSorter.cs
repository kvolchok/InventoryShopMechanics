using System;
using System.Collections.Generic;
using InventorySystem.Item;
using JetBrains.Annotations;
using UnityEngine;

namespace InventorySystem
{
    public class ItemsGroupSorter : MonoBehaviour
    {
        public event Action ItemsShowed;

        public ItemType SelectedMenu { get; private set; } = ItemType.All;

        [SerializeField]
        private GameObject[] _underlines;
        
        private List<ItemView> _itemViews;

        public void Initialize(List<ItemView> itemViews)
        {
            _itemViews = itemViews;
        }
        
        [UsedImplicitly]
        public void ShowItemsAllTypes()
        {
            SelectedMenu = ItemType.All;
            ShowAllItemsByType(SelectedMenu);
        }

        [UsedImplicitly]
        public void ShowItemsTypeWeapon()
        {
            SelectedMenu = ItemType.Weapon;
            ShowAllItemsByType(SelectedMenu);
        }
        
        [UsedImplicitly]
        public void ShowItemsTypeArmor()
        {
            SelectedMenu = ItemType.Armor;
            ShowAllItemsByType(SelectedMenu);
        }
        
        [UsedImplicitly]
        public void ShowItemsTypeMovement()
        {
            SelectedMenu = ItemType.Movement;
            ShowAllItemsByType(SelectedMenu);
        }
        
        [UsedImplicitly]
        public void ShowItemsTypeAccessory()
        {
            SelectedMenu = ItemType.Accessory;
            ShowAllItemsByType(SelectedMenu);
        }

        [UsedImplicitly]
        public void HideUnderlines()
        {
            foreach (var underline in _underlines)
            {
                underline.SetActive(false);
            }
        }

        public void ShowAllItemsByType(ItemType itemType)
        {
            if (AreItemsNull())
            {
                return;
            }
            
            var needToActivate = true;
                
            foreach (var itemView in _itemViews)
            {
                if (itemType != ItemType.All)
                {
                    needToActivate = itemView.Type == itemType;
                }

                itemView.gameObject.SetActive(needToActivate);
            }

            ItemsShowed?.Invoke();
        }
        
        private bool AreItemsNull()
        {
            return _itemViews == null;
        }
    }
}