using Hero;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Views
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _nameText;
        [SerializeField] 
        private TextMeshProUGUI _levelText;
        [SerializeField] 
        private TextMeshProUGUI _experienceText;

        private ShopView _shopView;
        private InventoryView _inventoryView;

        public void Initialize(HeroesSettings heroSettings, ShopView shopView, InventoryView inventoryView)
        {
            _nameText.text = heroSettings.Name;
            _levelText.text = heroSettings.Level.ToString();
            _experienceText.text = heroSettings.Experience.ToString();
            
            _shopView = shopView;
            _inventoryView = inventoryView;
        }
        
        [UsedImplicitly]
        public void ShowShopScreen()
        {
            gameObject.SetActive(false);
            _shopView.gameObject.SetActive(true);
            _shopView.ShowScreen();
        }
        
        [UsedImplicitly]
        public void ShowInventoryScreen()
        {
            gameObject.SetActive(false);
            _inventoryView.gameObject.SetActive(true);
            _inventoryView.ShowScreen();
        }
    }
}