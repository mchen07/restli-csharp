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

using restlicsharpdata.restlidata;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace restlicsharpclient.restliclient.util
{
    /// <summary>
    /// Util class for converting values relevant to the rest client.
    /// </summary>
    class ValueConverter
    {
        /// <summary>
        /// Converts a string to a value of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to be produced from the input string.</typeparam>
        /// <returns>The converted value</returns>
        public static T CoerceString<T>(string value)
        {
            if (typeof(T).IsAssignableFrom(typeof(RecordTemplate)))
            {
                return (T)ClientUtil.BuildRecord<RecordTemplate>(null);
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }
    }
}