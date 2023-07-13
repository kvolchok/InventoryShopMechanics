using System.Collections.Generic;
using Extensions;
using InventorySystem.Item;
using Newtonsoft.Json;

namespace Api.Responses
{
    public class InventoryResponse
    {
        public string Content { get; set; }
        
        // Используется пользовательский конвертер JSON,
        // поскольку стандартная сериализация/десериализация в json не может обработать словарь 
        [JsonConverter(typeof(DictionaryJsonConverter<ItemModel, int>))]
        public Dictionary<ItemModel, int> UserItems { get; set; }
    }
}