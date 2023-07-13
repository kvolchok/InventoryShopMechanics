using Api.Services;
using Extensions;

namespace Api
{
    public class WebApi : Singleton<WebApi>
    {
        public string JwtToken { get; set; }
        public AuthenticationApi AuthenticationAPI { get; private set; }
        public ShopApi ShopApi { get; private set; }
        public InventoryApi InventoryApi { get; private set; }
        public UserMoneyApi UserMoneyApi { get; private set; }
        
        protected override void Awake()
        {
            CreateApiServices();
        }

        private void CreateApiServices()
        {
            AuthenticationAPI = new AuthenticationApi();
            ShopApi = new ShopApi();
            InventoryApi = new InventoryApi();
            UserMoneyApi = new UserMoneyApi();
        }
    }
}