using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Extensions
{ 
    /// <summary>
    /// Класс-конвертер JSON для сериализации и десериализации словаря (Dictionary<TKey, TValue>).
    /// </summary>
    /// <typeparam name="TKey">Тип ключа словаря.</typeparam>
    /// <typeparam name="TValue">Тип значения словаря.</typeparam>
    public class DictionaryJsonConverter<TKey, TValue> : JsonConverter where TKey : class
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Dictionary<TKey, TValue>));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var itemDictionary = (Dictionary<TKey, TValue>)value;

            JArray array = new JArray();
            foreach (var kv in itemDictionary)
            {
                JObject obj = new JObject
                {
                    { "Key", JToken.FromObject(kv.Key, serializer) },
                    { "Value", JToken.FromObject(kv.Value, serializer) }
                };
                array.Add(obj);
            }

            array.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var array = JArray.Load(reader);
            var itemDictionary = new Dictionary<TKey, TValue>();
            foreach (JObject obj in array.Children<JObject>())
            {
                var key = obj["Key"].ToObject<TKey>(serializer);
                var value = obj["Value"].ToObject<TValue>(serializer);
                itemDictionary[key] = value;
            }

            return itemDictionary;
        }
    }
}