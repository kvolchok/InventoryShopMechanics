using System.Linq;
using Api;
using Hero;
using InventorySystem.Item;
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

        private void Awake()
        {
            _shopView.Initialize();
            _inventoryView.Initialize();

            _startButton.onClick.AddListener(ShowStartScreen);
            _authenticationView.Authorized += OnAuthorized;
            _authenticationView.Unauthorized += OnUnauthorized;
            _shopView.ItemBoughtSuccessfully += OnItemBought;
        }
        
        private void ShowStartScreen()
        {
            WebApi.Instance.AuthenticationAPI
                .SendLoginRequestByDeviceId(OnAuthorized, OnUnauthorized);
            _startScreenView.OnGameStarted();
        }

        private void OnAuthorized(UserProfile userProfile)
        {
            _startScreenView.OnAuthorized(_authenticationView);
            
            var selectedHeroSettings = userProfile.HeroesSettings
                .FirstOrDefault(hero => hero.IsSelected);

            _heroManager.Initialize(selectedHeroSettings);
            _currencyManager.Initialize(userProfile.Money, userProfile.Gems);
            _lobbyView.Initialize(selectedHeroSettings, _shopView, _inventoryView);
        }
        
        private void OnUnauthorized(string message)
        {
            NotificationsManager.Instance.ShowNotification(message);
        }

        private void OnItemBought(ItemModel itemModel)
        {
            _currencyManager.ItemBought(itemModel.Price);
            _inventoryView.AddItem(itemModel);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _authenticationView.Authorized -= OnAuthorized;
            _authenticationView.Unauthorized -= OnUnauthorized;
            _shopView.ItemBoughtSuccessfully -= OnItemBought;
        }
    }
}