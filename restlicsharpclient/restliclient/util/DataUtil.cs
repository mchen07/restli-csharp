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

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using restlicsharpdata.restlidata;

namespace restlicsharpclient.restliclient.util
{
    /// <summary>
    /// Util class for converting data relevant to the client.
    /// </summary>
    class DataUtil
    {
        /// <summary>
        /// Converts a string to a value of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to be produced from the input string.</typeparam>
        /// <returns>The converted value</returns>
        public static T CoerceString<T>(string value)
        {
            if (typeof(RecordTemplate).IsAssignableFrom(typeof(T)))
            {
                // TODO: Support complex key types
                throw new NotImplementedException("Coercing complex keys from string not yet supported.");
            }
            else if (typeof(EnumTemplate).IsAssignableFrom(typeof(T)))
            {
                return BuildEnum<T>(value);
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        /// <summary>
        /// Builds a RecordTemplate of specified Record type from data map.
        /// </summary>
        /// <typeparam name="TRecord">The type of Record to build, must be a <see cref="RecordTemplate"/></typeparam>
        /// <param name="dataMap">Data map used to build the record</param>
        /// <returns>Record built using data map</returns>
        public static TRecord BuildRecord<TRecord>(Dictionary<string, object> dataMap)
        {
            if (!typeof(RecordTemplate).IsAssignableFrom(typeof(TRecord)))
            {
                throw new ArgumentException(String.Format("Type {0} does not implement the RecordTemplate interface.", typeof(TRecord).ToString()));
            }
            ConstructorInfo constructor = typeof(TRecord).GetConstructor(new[] { typeof(Dictionary<string, object>) });
            return (TRecord)constructor.Invoke(new object[] { dataMap });
        }

        /// <summary>
        /// Builds an EnumTemplate of specified Enum type from data string.
        /// </summary>
        /// <typeparam name="TEnum">The type of Enum to build, must be an <see cref="EnumTemplate"/></typeparam>
        /// <param name="data">Data string used to build the enum</param>
        /// <returns>Enum built using data string</returns>
        public static TEnum BuildEnum<TEnum>(string data)
        {
            if (!typeof(EnumTemplate).IsAssignableFrom(typeof(TEnum)))
            {
                throw new ArgumentException(String.Format("Type {0} does not implement the EnumTemplate interface.", typeof(TEnum).ToString()));
            }
            ConstructorInfo constructor = typeof(TEnum).GetConstructor(new[] { typeof(string) });
            return (TEnum)constructor.Invoke(new object[] { data });
        }

        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <param name="dataObject">Object to be serialized</param>
        /// <returns>Object serialized as a string</returns>
        public static string SerializeObject(object dataObject)
        {
            return JsonConvert.SerializeObject(dataObject);
        }

        /// <summary>
        /// Serializes an object to bytes.
        /// </summary>
        /// <param name="dataObject">Object to be serialized</param>
        /// <returns>Object serialized as bytes</returns>
        public static byte[] SerializeObjectToBytes(object dataObject)
        {
            byte[] serialized;
            using (Stream bodyStream = new MemoryStream())
            using (StreamWriter streamWriter = new StreamWriter(bodyStream))
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                JsonSerializer.Create().Serialize(jsonTextWriter, dataObject);
                jsonTextWriter.Flush();
                serialized = bodyStream.ReadAllBytes();
            }
            return serialized;
        }

        /// <summary>
        /// Deserializes an object from string.
        /// </summary>
        /// <typeparam name="TObject">Type of object being deserialized</typeparam>
        /// <param name="dataString">String data to be used in object deserialization</param>
        /// <returns>Deserialized object</returns>
        public static TObject DeserializeObject<TObject>(string dataString)
        {
            return (TObject)JsonConvert.DeserializeObject<object>(dataString, new JsonConverter[] { new DataMapDeserializationConverter() });
        }
    }
}