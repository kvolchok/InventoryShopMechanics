using System.Linq;
using Api;
using Hero;
using Money;
using UnityEngine;
using UnityEngine.UI;
using User;
using Views;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;
        [SerializeField]
        private StartScreenView _startScreenView;
        [SerializeField]
        private AuthenticationView _authenticationView;
        
        [SerializeField]
        private CurrencyManager _currencyManager;
        [SerializeField]
        private HeroManager _heroManager;
        
        [SerializeField]
        private LobbyView _lobbyView;
        [SerializeField]
        private ShopView _shopView;
        [SerializeField]
        private InventoryView _inventoryView;
        
        [SerializeField]
        private NotificationsManager _notificationsManager;

        private UserProfile _userProfile;

        private void Awake()
        {
            _shopView.Initialize();
            _inventoryView.Initialize();
            _notificationsManager.Initialize(_shopView, _inventoryView, _currencyManager);
            
            _startButton.onClick.AddListener(ShowStartScreen);
            _authenticationView.Authorized += OnAuthorized;
            _authenticationView.Unauthorized += OnUnauthorized;
            _shopView.OnItemBought += OnMoneyChanged;
            _currencyManager.MoneyChanged += OnMoneyChanged;
        }
        
        private void ShowStartScreen()
        {
            WebApi.Instance.AuthenticationAPI.SendLoginRequestByDeviceId(OnAuthorized, OnUnauthorized);
            _startScreenView.OnGameStarted();
        }

        private void OnAuthorized(UserProfile userProfile)
        {
            _startScreenView.OnAuthorized(_authenticationView);
            
            _userProfile = userProfile;
            var selectedHeroSettings = _userProfile.HeroesSettings.FirstOrDefault(hero => hero.IsSelected);

            _heroManager.Initialize(selectedHeroSettings);
            _currencyManager.Initialize(_userProfile.Money, _userProfile.Gems);
            
            _lobbyView.Initialize(selectedHeroSettings, _shopView, _inventoryView);
        }
        
        private void OnUnauthorized(string message)
        {
            _notificationsManager.ShowNotification(message);
        }

        private void OnMoneyChanged(int money)
        {
            _userProfile.Money += money;
            _currencyManager.Initialize(_userProfile.Money, _userProfile.Gems);
        }
        
        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _authenticationView.Authorized -= OnAuthorized;
            _authenticationView.Unauthorized -= OnUnauthorized;
            _shopView.OnItemBought -= OnMoneyChanged;
            _currencyManager.MoneyChanged -= OnMoneyChanged;
        }
    }
}