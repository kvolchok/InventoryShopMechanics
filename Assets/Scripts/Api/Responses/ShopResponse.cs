using System.Collections.Generic;
using InventorySystem.Item;

namespace Api.Responses
{
    public class ShopResponse
    {
        public string Content { get; set; }
        
        public ItemModel Item { get; set; }
        public List<ItemModel> GameItems { get; set; }
    }
}