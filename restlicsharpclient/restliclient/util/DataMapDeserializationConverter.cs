using System;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace restlicsharpclient.restliclient.util
{
    class DataMapDeserializationConverter : CustomCreationConverter<object>
    {
        public override object Create(Type objectType)
        {
            if (objectType == typeof(Array))
                return new List<object>();
            else
                return new Dictionary<string, object>();
        }

        public override bool CanConvert(Type objectType)
        {
            // in addition to handling Dictionary<string, object>
            // we want to handle the deserialization of dict value
            // which is of type object
            return objectType == typeof(object) || base.CanConvert(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
                return base.ReadJson(reader, typeof(Array), existingValue, serializer);
            else if (reader.TokenType == JsonToken.StartObject || reader.TokenType == JsonToken.Null)
                return base.ReadJson(reader, objectType, existingValue, serializer);

            // if next token is not object or array, use standard deserializer
            return serializer.Deserialize(reader);
        }
    }
}
