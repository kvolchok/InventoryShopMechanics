namespace Api
{
    public class Endpoints
    {
        public const string API_URL = "http://localhost:5212/api/";
        public const string LOGIN_URL = "authentication/login";
        public const string REGISTRATION_URL = "authentication/registration";

        public const string DELETE_ITEM_URL = "inventory/delete/id=";
        public const string GET_USER_ITEMS_URL = "inventory/items";
        
        public const string BUY_ITEM_URL = "shop/buy/id=";
        public const string GET_ALL_GAME_ITEMS_URL = "shop/items/";

        public const string ADD_MONEY_URL = "usermoney/add-money";
    }
}