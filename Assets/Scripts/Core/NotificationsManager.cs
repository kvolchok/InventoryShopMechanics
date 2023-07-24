using Money;
using UnityEngine;
using Views;

namespace Core
{
    public class NotificationsManager : MonoBehaviour
    {
        [SerializeField]
        private NotificationView _notificationViewPrefab;
        [SerializeField]
        private Transform _notificationViewRoot;
        
        private ShopView _shopView;
        private InventoryView _inventoryView;
        private CurrencyManager _currencyManager;

        public void Initialize(ShopView shopView, InventoryView inventoryView, CurrencyManager currencyManager)
        {
            _shopView = shopView;
            _inventoryView = inventoryView;
            _currencyManager = currencyManager;
            
            _shopView.ItemBoughtEvent += ShowNotification;
            _shopView.ErrorEvent += ShowNotification;
            _inventoryView.OnItemDeletedSuccessful += ShowNotification;
            _inventoryView.ErrorEvent += ShowNotification;
            _currencyManager.ErrorEvent += ShowNotification;
        }

        public void ShowNotification(string message)
        {
            var notificationView = Instantiate(_notificationViewPrefab, _notificationViewRoot);
            notificationView.Initialize(message);
        }

        private void OnDestroy()
        {
            _shopView.ItemBoughtEvent -= ShowNotification;
            _shopView.ErrorEvent -= ShowNotification;
            _inventoryView.OnItemDeletedSuccessful -= ShowNotification;
            _inventoryView.ErrorEvent -= ShowNotification;
            _currencyManager.ErrorEvent -= ShowNotification;
        }
    }
}