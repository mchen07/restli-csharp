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
using System.Linq;
using System.Text.RegularExpressions;

using restlicsharpdata.restlidata;

namespace restlicsharpclient.restliclient.request.url
{
    /// <summary>
    /// Various utility methods to be used when constructing a Request URL.
    /// </summary>
    public static class UrlParamUtil
    {
        internal static string EncodeDataObject(object data, UrlConstants.EncodingContext encodingContext)
        {
            if (data is Dictionary<string, object>)
            {
                return EncodeDataMap((Dictionary<string, object>)data, encodingContext);
            }
            else if (data is List<object>)
            {
                return EncodeDataList((List<object>)data, encodingContext);
            }
            else if (data is RecordTemplate)
            {
                return EncodeDataObject((data as RecordTemplate).Data(), encodingContext);
            }
            else if (data is EnumTemplate)
            {
                return EncodeDataObject((data as EnumTemplate).Data(), encodingContext);
            }
            else if (data is UnionTemplate)
            {
                return EncodeDataObject((data as UnionTemplate).Data(), encodingContext);
            }
            else if (data is Bytes)
            {
                return EncodeDataObject((data as Bytes).Data(), encodingContext);
            }
            else if (data is bool)
            {
                return ((bool)data) ? "true" : "false";
            }
            else
            {
                return EncodeString(data.ToString(), encodingContext);
            }
        }

        internal static string EncodeDataMap(Dictionary<string, object> dataMap, UrlConstants.EncodingContext encodingContext)
        {
            List<string> encodedElements = new List<string>();
            foreach (KeyValuePair<string, object> pair in dataMap)
            {
                encodedElements.Add(EncodeMapPair(pair.Key, pair.Value, encodingContext));
            }
            // Sort and join with the Rest.li encoded map delimiter
            encodedElements.Sort();
            return String.Format("{0}{1}{2}", UrlConstants.kObjectStart, String.Join(UrlConstants.kItemSep, encodedElements), UrlConstants.kObjectEnd);
        }

        private static string EncodeMapPair(string key, object value, UrlConstants.EncodingContext encodingContext)
        {
            return String.Format("{0}{1}{2}", EncodeString(key, encodingContext), UrlConstants.kKeyValueSep, EncodeDataObject(value, encodingContext));
        }

        internal static string EncodeDataList(List<object> dataList, UrlConstants.EncodingContext encodingContext)
        {
            List<string> encodedElements = new List<string>();
            foreach (object element in dataList)
            {
                encodedElements.Add(EncodeDataObject(element, encodingContext));
            }
            return String.Format("{0}{1}{2}{3}", UrlConstants.kListPrefix, UrlConstants.kObjectStart, String.Join(UrlConstants.kItemSep, encodedElements), UrlConstants.kObjectEnd);
        }

        internal static string EncodeString(string data, UrlConstants.EncodingContext encodingContext)
        {
            if (data.Length == 0)
            {
                return UrlConstants.kEmptyStrRep;
            }
            else
            {
                string reservedCharsRegex;
                switch (encodingContext)
                {
                    case UrlConstants.EncodingContext.Path:
                        reservedCharsRegex = UrlConstants.kPathReservedCharsRegex;
                        break;
                    case UrlConstants.EncodingContext.Query:
                        reservedCharsRegex = UrlConstants.kQueryReservedCharsRegex;
                        break;
                    default:
                        reservedCharsRegex = "";
                        break;
                }
                return Regex.Replace(Uri.EscapeUriString(data), reservedCharsRegex, _ => Uri.HexEscape(Convert.ToChar(_.Value.ToString())));
            }
        }

        public static string EncodeQueryParams(Dictionary<string, object> dataMap)
        {
            Dictionary<string, string> encodedParams = DataMapToEncodedQueryParams(dataMap);
            return JoinQueryItems(encodedParams);
        }

        private static Dictionary<string, string> DataMapToEncodedQueryParams(Dictionary<string, object> dataMap)
        {
            Dictionary<string, string> encodedParams = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> pair in dataMap)
            {
                encodedParams.Add(EncodeString(pair.Key, UrlConstants.EncodingContext.Query), EncodeDataObject(pair.Value, UrlConstants.EncodingContext.Query));
            }
            return encodedParams;
        }

        private static string JoinQueryItems(Dictionary<string, string> dataMap)
        {
            List<string> encodedQueryItems = new List<string>();
            foreach (KeyValuePair<string, string> pair in dataMap)
            {
                 encodedQueryItems.Add(String.Format("{0}{1}{2}", pair.Key, UrlConstants.kQueryKeyValueSep, pair.Value));
            }
            // Sort and join with &
            encodedQueryItems.Sort();
            return String.Join(UrlConstants.kQueryItemSep, encodedQueryItems);
        }

        public static Dictionary<string, string> EncodePathKeysForUrl(IReadOnlyDictionary<string, object> pathKeys)
        {
            return pathKeys.ToDictionary(_ => _.Key, _ => EncodeDataObject(_.Value, UrlConstants.EncodingContext.Path));
        }

        public static string EncodeRequestKeyForPath(object requestKey)
        {
            return EncodeDataObject(requestKey, UrlConstants.EncodingContext.Path);
        }
    }
}
