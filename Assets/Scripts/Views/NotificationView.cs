using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Views
{
    public class NotificationView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _message;

        public void Initialize(string message)
        {
            _message.text = message;
        }
        
        [UsedImplicitly]
        public void CloseScreen()
        {
            Destroy(gameObject);    
        }
    }
}