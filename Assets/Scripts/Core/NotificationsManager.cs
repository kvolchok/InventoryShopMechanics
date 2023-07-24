using Extensions;
using Money;
using UnityEngine;
using Views;

namespace Core
{
    public class NotificationsManager : Singleton<NotificationsManager>
    {
        private NotificationView _notificationViewPrefab;
        private Transform _notificationViewRoot;
        
        private ShopView _shopView;
        private InventoryView _inventoryView;
        private CurrencyManager _currencyManager;

        public void Initialize(NotificationView notificationViewPrefab, Transform notificationViewRoot)
        {
            _notificationViewPrefab = notificationViewPrefab;
            _notificationViewRoot = notificationViewRoot;
        }

        public void ShowNotification(string message)
        {
            var notificationView = Instantiate(_notificationViewPrefab, _notificationViewRoot);
            notificationView.Initialize(message);
        }
    }
}