using System.Linq;
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

        private void Start()
        {
            _startScreenView.Initialize(_authenticationView);
            _notificationsManager.Initialize(_startScreenView, _shopView, _inventoryView, _currencyManager);
            
            _startButton.onClick.AddListener(ShowStartScreen);
            _startScreenView.Authorized += OnAuthorized;
            _shopView.OnItemBought += OnMoneyChanged;
            _currencyManager.MoneyChanged += OnMoneyChanged;
        }
        
        private void ShowStartScreen()
        {
            _startScreenView.OnGameStarted();
        }

        private void OnAuthorized(UserProfile userProfile)
        {
            _userProfile = userProfile;
            var selectedHeroSettings = _userProfile.HeroesSettings.FirstOrDefault(hero => hero.IsSelected);

            _heroManager.Initialize(selectedHeroSettings);
            _currencyManager.Initialize(_userProfile.Money, _userProfile.Gems);

            _shopView.Initialize();
            _inventoryView.Initialize();
            _lobbyView.Initialize(selectedHeroSettings, _shopView, _inventoryView);
        }

        private void OnMoneyChanged(int money)
        {
            _userProfile.Money += money;
            _currencyManager.Initialize(_userProfile.Money, _userProfile.Gems);
        }
        
        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _startScreenView.Authorized -= OnAuthorized;
            _shopView.OnItemBought -= OnMoneyChanged;
            _currencyManager.MoneyChanged -= OnMoneyChanged;
        }
    }
}