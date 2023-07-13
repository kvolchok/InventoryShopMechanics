using System;
using InventorySystem.Item;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class ItemView : MonoBehaviour
    {
        private Action<ItemModel> _onClick;
        
        public ItemType Type => _itemModel.ItemType;

        [SerializeField]
        private Image _background;
        [SerializeField]
        private Image _itemIcon;
        [SerializeField]
        private TextMeshProUGUI _itemQuantity;

        private ItemModel _itemModel;

        public void Initialize(Sprite background, ItemModel itemModel, Action<ItemModel> onClick, int quantity = 0)
        {
            _background.sprite = background;
            
            _itemModel = itemModel;
            _itemIcon.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(_itemModel.SpritePath);
            
            _onClick = onClick;
            
            if (quantity > 1)
            {
                _itemQuantity.text = quantity.ToString();
                _itemQuantity.gameObject.SetActive(true);
            }
        }

        [UsedImplicitly]
        public void OnClick()
        {
            _onClick?.Invoke(_itemModel);
        }
    }
}