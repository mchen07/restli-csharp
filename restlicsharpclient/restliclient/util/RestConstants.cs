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

namespace restlicsharpclient.restliclient.util
{
    /// <summary>
    /// Contains constants for use by the Rest Client.
    /// </summary>
    class RestConstants
    {
        public const string kStartParam = "start";
        public const string kCountParam = "count";
        public const string kQueryTypeParam = "q";

        public static readonly char[] kHeaderDelimiters = { ',' };
        public const string kHeaderDelimiter = ",";
        public const string kHeaderContentType = "Content-Type";
        public const string kHeaderValueApplicationJson = "application/json";
        public const string kHeaderRestliRequestMethod = "X-RestLi-Method";
        public const string kHeaderRestliErrorResponse = "X-RestLi-Error-Response";
        public const string kHeaderRestliProtocolVersion = "X-RestLi-Protocol-Version";
        public const string kHeaderRestliId = "X-RestLi-Id";
        public const string kRestLiVersion20 = "2.0.0";
        public const string kHeaderLocation = "Location";

        // HTTP status codes
        public const int httpStatusCreated = 201;
        public const int httpStatusInternalServerError = 500;
    }
}
