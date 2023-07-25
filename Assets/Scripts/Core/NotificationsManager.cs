using Extensions;
using UnityEngine;
using Views;

namespace Core
{
    public class NotificationsManager : Singleton<NotificationsManager>
    {
        [SerializeField]
        private NotificationView _notificationViewPrefab;
        [SerializeField]
        private Transform _notificationViewRoot;
        
        public void ShowNotification(string message)
        {
            var notificationView = Instantiate(_notificationViewPrefab, _notificationViewRoot);
            notificationView.Initialize(message);
        }
    }
}