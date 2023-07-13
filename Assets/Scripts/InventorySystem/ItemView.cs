using System;
using InventorySystem.Item;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class ItemView : MonoBehaviour
    {
        private Action<ItemModel> _onClick;
        
        public ItemType Type => _itemModelModel.ItemType;

        [SerializeField]
        private Image _background;
        [SerializeField]
        private Image _itemIcon;
        [SerializeField]
        private TextMeshProUGUI _itemQuantity;

        private ItemModel _itemModelModel;
        private int _quantity;

        public void Initialize(Sprite background, ItemModel itemModelModel, Action<ItemModel> onClick, int quantity = 0)
        {
            _background.sprite = background;
            
            _itemModelModel = itemModelModel;
            _itemIcon.sprite = Resources.Load<Sprite>(_itemModelModel.SpritePath);
            
            _onClick = onClick;
            
            if (quantity > 1)
            {
                _quantity = quantity;
                _itemQuantity.text = _quantity.ToString();
                _itemQuantity.gameObject.SetActive(true);
            }
        }

        [UsedImplicitly]
        public void OnClick()
        {
            _onClick?.Invoke(_itemModelModel);
        }
    }
}