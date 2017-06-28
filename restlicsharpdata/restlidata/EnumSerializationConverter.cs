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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace restlicsharpdata.restlidata
{
    /// <summary>
    /// Provides a custom specification for how the JSON.NET package serializes <see cref="EnumTemplate"/> instances.
    /// </summary>
    class EnumSerializationConverter : JsonConverter
    {
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EnumTemplate);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is EnumTemplate)
            {
                dynamic enumObject = value;
                writer.WriteValue(enumObject.symbol.ToString());
            }
            else
            {
                JObject o = (JObject)value;
                o.WriteTo(writer);
            }
        }
    }
}
