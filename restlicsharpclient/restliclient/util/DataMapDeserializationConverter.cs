/*
   Copyright (c) 2017 LinkedIn Corp.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace restlicsharpclient.restliclient.util
{
    /// <summary>
    /// Custom implementation of a CustomCreationConverter used by the JSON.NET library.
    /// <para>Performs deserialization from a JSON string to a C# in-memory data map.</para>
    /// </summary>
    class DataMapDeserializationConverter : CustomCreationConverter<object>
    {
        /// <summary>
        /// <para>This method takes an object type and returns an empty instance of that type to be populated.</para> 
        /// <para>Since this method is only called by the base class method <see cref="CustomCreationConverter{T}.ReadJson"/>
        /// for complex types (object, array, null), primitives do not have to be handled.</para>
        /// </summary>
        /// <param name="objectType">The object type for which to create an empty instance</param>
        /// <returns>An empty instance of object type objectType</returns>
        public override object Create(Type objectType)
        {
            if (objectType == typeof(List<object>))
                return new List<object>();
            else if (objectType == typeof(Dictionary<string, object>))
                return new Dictionary<string, object>();
            else
                return null;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// <para>This converter can handle the conversion of Dictionaries, Lists, and dict values, which are of objectType object</para>
        /// </summary>
        /// <param name="objectType">The object type for which convertibility is being inspected</param>
        /// <returns>Boolean indicating whether objectType can be converted</returns>
        public override bool CanConvert(Type objectType)
        {
            // In addition to handling Dictionary<string, object> and List<object>,
            // we want to handle the deserialization of dict values which are of type object
            return objectType == typeof(object) ||
                typeof(Dictionary<string, object>).IsAssignableFrom(objectType) ||
                typeof(List<object>).IsAssignableFrom(objectType); ;
        }

        /// <summary>
        /// <para>Reads the JSON value at the current token in the reader.</para>
        /// <para>Calls base class method <see cref="CustomCreationConverter{T}.ReadJson"/> if at opening tokens for complex types (object, array, null).</para>
        /// <para>If the current token is a primitive, use the standard deserializer.</para>
        /// </summary>
        /// <returns>The object represented at this token in the reader</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
                return base.ReadJson(reader, typeof(List<object>), existingValue, serializer);
            else if (reader.TokenType == JsonToken.StartObject)
                return base.ReadJson(reader, typeof(Dictionary<string, object>), existingValue, serializer);
            else if (reader.TokenType == JsonToken.Null)
                return base.ReadJson(reader, objectType, existingValue, serializer);

            // If next token is not object or array, use standard deserializer
            return serializer.Deserialize(reader);
        }
    }
}
