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
using System.Text;
using System.Text.RegularExpressions;

namespace restlicsharpclient.restliclient.util
{
    /// <summary>
    /// Various utility methods to be used when constructing a Request URL.
    /// </summary>
    public static class UrlParamUtil
    {

        public static string EncodeDataObject(object data)
        {
            if (data is Dictionary<string, object>)
            {
                return EncodeDataMap((Dictionary<string, object>)data);
            }
            else if (data is List<object>)
            {
                return EncodeDataList((List<object>)data);
            }
            else
            {
                return EncodeString(data.ToString());
            }
        }

        public static string EncodeDataMap(Dictionary<string, object> dataMap)
        {
            List<string> encodedElements = new List<string>();
            foreach (KeyValuePair<string, object> pair in dataMap)
            {
                encodedElements.Add(EncodeMapPair(pair.Key, pair.Value));
            }
            return String.Format("{0}{1}{2}", UrlConstants.kObjectStart, String.Join(UrlConstants.kItemSep, encodedElements), UrlConstants.kObjectEnd);
        }

        public static string EncodeMapPair(string key, object value)
        {
            return String.Format("{0}{1}{2}", EncodeString(key), UrlConstants.kKeyValueSep, EncodeDataObject(value));
        }

        public static string EncodeDataList(List<object> dataList)
        {
            List<object> encodedElements = new List<object>();
            foreach (object element in dataList)
            {
                encodedElements.Add(EncodeDataObject(element));
            }
            return String.Format("{0}{1}{2}{3}", UrlConstants.kListPrefix, UrlConstants.kObjectStart, String.Join(UrlConstants.kItemSep, encodedElements), UrlConstants.kObjectEnd);
        }

        public static string EncodeString(string data)
        {
            if (data.Length == 0)
            {
                return UrlConstants.kEmptyStrRep;
            }
            else
            {
                return Regex.Replace(Uri.EscapeUriString(data), UrlConstants.kReservedCharsRegex, _ => Uri.HexEscape(Convert.ToChar(_.Value.ToString())));
            }
        }

        public static string EncodeQueryParams(Dictionary<string, object> dataMap)
        {
            Dictionary<string, string> encodedParams = DataMapToQueryParams(dataMap);
            return JoinQueryItems(encodedParams);
        }

        public static Dictionary<string, string> DataMapToQueryParams(Dictionary<string, object> dataMap)
        {
            Dictionary<string, string> encodedParams = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> pair in dataMap)
            {
                encodedParams.Add(EncodeString(pair.Key), EncodeDataObject(pair.Value));
            }
            return encodedParams;
        }

        public static string JoinQueryItems(Dictionary<string, string> dataMap)
        {
            List<string> encodedQueryItems = new List<string>();
            foreach (KeyValuePair<string, string> pair in dataMap)
            {
                 encodedQueryItems.Add(String.Format("{0}{1}{2}", pair.Key, UrlConstants.kQueryKeyValueSep, pair.Value));
            }
            // Join with &, but don't sort (too expensive)
            return String.Format("{0}{1}", UrlConstants.kQueryParamsBegin, String.Join(UrlConstants.kQueryItemSep, encodedQueryItems));
        }
    }
}
