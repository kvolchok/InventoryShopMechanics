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

        private StartScreenView _startScreen;
        private ShopView _shopView;
        private InventoryView _inventoryView;
        private CurrencyManager _currencyManager;

        public void Initialize(StartScreenView startScreen, ShopView shopView, InventoryView inventoryView,
            CurrencyManager currencyManager)
        {
            _startScreen = startScreen;
            _shopView = shopView;
            _inventoryView = inventoryView;
            _currencyManager = currencyManager;

            _startScreen.Unauthorized += ShowNotification;
            _shopView.ItemBoughtEvent += ShowNotification;
            _shopView.ErrorEvent += ShowNotification;
            _inventoryView.OnItemDeletedSuccessful += ShowNotification;
            _inventoryView.ErrorEvent += ShowNotification;
            _currencyManager.ErrorEvent += ShowNotification;
        }

        private void ShowNotification(string message)
        {
            var notificationView = Instantiate(_notificationViewPrefab, _notificationViewRoot);
            notificationView.Initialize(message);
        }

        private void OnDestroy()
        {
            _startScreen.Unauthorized -= ShowNotification;
            _shopView.ItemBoughtEvent -= ShowNotification;
            _shopView.ErrorEvent -= ShowNotification;
            _inventoryView.OnItemDeletedSuccessful -= ShowNotification;
            _inventoryView.ErrorEvent -= ShowNotification;
            _currencyManager.ErrorEvent -= ShowNotification;
        }
    }
}