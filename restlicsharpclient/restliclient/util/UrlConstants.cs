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

namespace restlicsharpclient.restliclient.util
{
    /// <summary>
    /// Contains constants for use when constructing a Rest.li encoded URL.
    /// </summary>
    public static class UrlConstants
    {
        public const string kObjectStart = "(";
        public const string kObjectEnd = ")";
        public const string kKeyValueSep = ":";
        public const string kItemSep = ",";
        public const string kListPrefix = "List";
        public const string kEmptyStrChar = "'";
        public const string kEmptyStrRep = kEmptyStrChar + kEmptyStrChar;

        public const string kQueryKeyValueSep = "=";
        public const string kQueryItemSep = "&";
        public const string kQueryParamsBegin = "?";
        public const string kPathSep = "/";

        public static readonly string[] RESERVED_CHARS = { kObjectStart, kObjectEnd, kKeyValueSep, kItemSep, kEmptyStrChar, kQueryKeyValueSep, kQueryItemSep, kPathSep };

        public static readonly string kReservedCharsRegex = String.Format("[{0}]", String.Join("", RESERVED_CHARS));
    }
}
